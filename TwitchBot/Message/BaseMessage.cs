using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class BaseMessage : Classes.PropDict
	{
		/// <summary>
		/// 
		/// </summary>
		protected Classes.BotBase bot;

		/// <summary>
		/// Parses the incoming message into the appropriate message class
		/// </summary>
		/// <param name="bot"></param>
		/// <param name="msgStr"></param>
		/// <returns>Parsed message object</returns>
		public static Message ParseMessageString(Classes.BotBase bot, string msgStr)
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
		
		/// <summary>
		/// Simple Constructor so that in derived classes you don't have to create a constructor
		/// </summary>
		/// <param name="bot"></param>
		/// <param name="raw"></param>
		public virtual void Init(Classes.BotBase bot, string raw)
		{
			this.bot = bot;
			this.raw = raw;
			this.raw = this.raw.TrimEnd('\r', '\n');
			time = DateTime.UtcNow;
		}

		/// <summary>
		/// Time the message was recived
		/// </summary>
		public DateTime time { get; private set; }

		#region Utility
		/// <summary>
		/// The command that the message was sent on
		/// </summary>
		public virtual ECommand action { get; }

		/// <summary>
		/// Permission level of the message
		/// </summary>
		public virtual EPermissions permission { get; }

		/// <summary>
		/// The channel the message was sent to
		/// </summary>
		public virtual string channel { get; }

		/// <summary>
		/// The user the message was sent by: Possibly TMI/Twitch
		/// </summary>
		public virtual string user { get; }

		/// <summary>
		/// If the message was valid
		/// </summary>
		public virtual bool isValid { get; }

		/// <summary>
		/// If the message was sent by a twitch user account
		/// </summary>
		public virtual bool isUserMessage { get; }
		#endregion

		#region Message Data
		/// <summary>
		/// Tags From the parsed string, possibly null
		/// </summary>
		public virtual Dictionary<string, string> tags { get; }

		/// <summary>
		/// Prefix of the irc string, after tags and before payload
		/// </summary>
		public virtual string prefix { get; }

		/// <summary>
		/// Payload after prefix
		/// </summary>
		public virtual List<string> command { get; }

		/// <summary>
		/// The message after <see cref="command">command</see>: , Possibly Null
		/// </summary>
		public virtual string message { get; }

		/// <summary>
		/// Raw storage of the orignal parsed string
		/// </summary>
		public string raw { get; protected set; }
		#endregion

	} 
}
