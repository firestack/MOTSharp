using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MOT.Plugins
{
	public class BannedWordData
	{
		
		[Newtonsoft.Json.JsonProperty("reason")]
		public string reason = "Reminder: K appa and it's variants are all banned here, they show up as *** for everyone else but yourself. if you use it you will get purged/timed out.";

		[Newtonsoft.Json.JsonProperty("length")]
		protected uint length;

		public uint Length {
			get
			{
				// Timeout length clamp range for Twitch (1 second and 2 weeks)
				return Math.Max(Math.Min(length, 1209600), 1);
			}
			set
			{
				length = value;
			}
		}

		[Newtonsoft.Json.JsonProperty("word")]
		public string word;
	}


	[TwitchBot.Attributes.Command( accessLevel = TwitchBot.Message.EPermissions.TMI, respondsTo = TwitchBot.Message.ECommand.PRIVMSG)]
	class BannedWordOperator : TwitchBot.Classes.Plugin
	{
		BannedWordData cfg = new BannedWordData();

		public string reason = "Reminder: K appa and it's variants are all banned here, they show up as *** for everyone else but yourself. if you use it you will get purged/timed out.";

		[TwitchBot.Attributes.Range(1, 1209600)]
		public uint length = 1;

		public override void Invoke()
		{
			if (message.message.Contains("***"))
			{

				var a = cfg as BannedWordData;
				bot.PM(message.channel, string.Format("/timeout {0} {1} {2}", message.tags["display-name"], a.Length, a.reason));
				bot.Whisper(message.channel, message.user, a.reason);
			}
		}
	}
}
