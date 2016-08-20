using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
	public abstract class Tags : Message
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
}
