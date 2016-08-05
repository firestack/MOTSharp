using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
	public abstract class Message
	{
		public static Message ParseMessageString(Classes.Bot bot, string msgStr)
		{
			// Parse Base Message
			Message msg = null;
			if (msgStr.StartsWith("@"))
			{
				// Has Tags
				msg = new TagsMessage();
			}
			else if(msgStr.StartsWith("PING"))
			{
				// Is Ping
				msg = new PingMessage();
			}
			else
			{
				// Doesn't Have Tags
				msg = new NoTagsMessage();
			}

			if(msg != null)
			{
				msg.Init(bot, msgStr);
			}          

			return msg;
		}

		public virtual void Init(Classes.Bot bot, string raw)
		{
			this.bot = bot;
			this.raw = raw;
			this.raw = this.raw.TrimEnd('\r', '\n');
			time = DateTime.UtcNow;
		}
		// Info
		public DateTime time { get; private set; }
		protected Classes.Bot bot;
		
		// Utility
		public virtual ECommand action { get; }
		public virtual EPermissions permission { get; }
		public virtual string channel { get; }
		public virtual string user { get; }
		public virtual bool isValid { get; }
		public virtual bool isUserMessage { get; }

		// Message Data
		public virtual Dictionary<string, string> tags { get; }
		public virtual string prefix { get; }
		public virtual List<string> command { get; }
		public virtual string message { get; }


		public string raw { get; protected set; }
	} 

	public abstract class BaseMessage : Message
	{
		protected ECommand? actionCache;
		public override ECommand action
		{
			get
			{
				if (actionCache == null)
				{
					int tmpInt;
					ECommand TVal;
					if (int.TryParse(command[0], out tmpInt))
					{
						actionCache = ECommand.NUMERIC;
					}
					else if (Enum.TryParse(command[0], out TVal))
					{
						actionCache = TVal;
					}
					else
					{
						actionCache = ECommand.UNKNOWN;
					}
				}
				return (ECommand)actionCache;
			}
		}

		protected string channelCache;
		public override string channel
		{
			get
			{
				if(channelCache == null)
				{
					channelCache = new String(command[1].Skip(1).ToArray());
				}
				return channelCache;
			}
		}

		public override bool isValid { get { return !string.IsNullOrWhiteSpace(raw); } }

		public override bool isUserMessage { get { return action == ECommand.PRIVMSG; } }

		protected string prefixCache;
		public override string prefix
		{
			get
			{
				if (prefixCache == null)
				{
					prefixCache = bodyCache[0];
				}
				return prefixCache;
			}
		}

		protected List<string> commandCache;
		public override List<string> command
		{
			get
			{
				if(commandCache == null)
				{
					commandCache = body.GetRange(1, body.Count - 2);
				}
				return commandCache;
			}
		}

		protected string messageCache;
		public override string message
		{
			get
			{
				if(messageCache == null)
				{
					messageCache = body.Last();
				}
				return messageCache;
			}
		}

		protected List<string> bodyCache;
		protected virtual List<string> body { get; }

		public override string ToString()
		{
			return raw;
		}

	}

	public abstract class NoTags : BaseMessage
	{
		public override bool isUserMessage { get { return false; } }

		public override Dictionary<string, string> tags { get { return null; } }

		protected override List<string> body
		{
			get
			{
				if(bodyCache == null)
				{
					var cleanBody = new String(raw.Skip(1).ToArray());
					bodyCache = new List<string>();

					var bodySplit = cleanBody.Split(":".ToCharArray(), 2, StringSplitOptions.None);
					bodyCache.AddRange(bodySplit[0].Split(' '));
					bodyCache.Add(bodySplit.Length == 1 ? "" : bodySplit[1]);
				}
				return bodyCache;
			}
		}
	}

	public abstract class Tags : BaseMessage
	{
		protected Dictionary<string, string> tagCache;
		public override Dictionary<string, string> tags
		{
			get
			{
				if (tagCache == null)
				{
					tagCache = new Dictionary<string, string>();
					var tagsString = new string(raw.Skip(1).ToArray());
					foreach (var KV in tagsString.Split(' ')[0].Split(';'))
					{
						var keyvalue = KV.Split('=');
						tagCache[keyvalue[0]] = keyvalue[1];
					}

				}
				return tagCache;
			}
		}

		protected override List<string> body
		{
			get
			{
				if (bodyCache == null)
				{
					// This gives the body of the message which isn't the tags
					// @tags=true :user!user@user.name PRIVMSG #channel :message
					//           ^ ^splitting there, returning this^^^^^^^^^^^^

					var bodyArray = raw.Split(new string[] { " :" }, 3, StringSplitOptions.None);
					bodyCache = new List<string>();

					bodyCache.AddRange(bodyArray[1].Split(' '));
					bodyCache.Add(bodyArray.Length == 2 ? "" : bodyArray[2]);
				}
				return bodyCache;
			}
		}

		protected string userCache;
		public override string user
		{
			get
			{
				if (userCache == null)
				{
					userCache = "";
					tags.TryGetValue("display-name", out userCache);
				}
				
				return userCache;
			}
		}
	}

	public class PingMessage : NoTags
	{
		// We should parse the message and double check
		// therefore we are compatible with all IRC spec
		// And WHEN twitch changes everything up, we will be ready

		public override ECommand action { get { return ECommand.PING; } }
		public override EPermissions permission { get { return EPermissions.TMI; } }
		public override string prefix { get { return "PING"; } }
		public override List<string> command { get { return (new string[] { "PING" }).ToList(); } }
		public override string message { get { return "tmi.twitch.tv"; } }
	}

	public class TagsMessage : Tags
	{
		protected EPermissions? permissionCache;
		public override EPermissions permission
		{
			get
			{
				if (permissionCache == null)
				{
					if (!isUserMessage)
					{
						permissionCache = EPermissions.TMI;
					}
					// HACK: Fix Superusers
					//else if (bot != null && bot.SuperUsers.Contains(tags["display-name"].ToLower()))
					//{
					//    permissionCache = EPermissions.SUPERUSER;
					//}
					else if (user.ToLower() == channel.ToLower())
					{
						permissionCache = EPermissions.BROADCASTER;
					}
					else if (tags["user-type"].Equals("mod"))
					{
						permissionCache = EPermissions.MOD;
					}
					//se if (tags["subscriber"])
					else
					{
						permissionCache = EPermissions.USER;
					}
				}
				return (EPermissions)permissionCache;
			}
		}

		public override string ToString()
		{
			if (action == ECommand.PRIVMSG)
				return "#" + channel + "::" + permission.ToString() + ":" + user + " :" + message;
			else
				return base.ToString();

		}
	}
	
	public class NoTagsMessage : NoTags
	{
		public override EPermissions permission { get { return EPermissions.TMI; } }
	}
}
