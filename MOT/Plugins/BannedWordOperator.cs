using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MOT.Plugins
{
	[TwitchBot.Attributes.Command( accessLevel = TwitchBot.Message.EPermissions.TMI, respondsTo = TwitchBot.Message.ECommand.PRIVMSG)]
	class BannedWordOperator : TwitchBot.Classes.Plugin
	{
		public string[] words = new string[0];
		
		public string reason = "Reminder: K appa and it's variants are all banned here, they show up as *** for everyone else but yourself. if you use it you will get purged/timed out.";

		[TwitchBot.Attributes.Range(1, 1209600)]
		public uint length = 1;

		public override void Invoke()
		{
			if (message.message.Contains("***"))
			{
				bot.PM(message.channel, string.Format("/timeout {0} {1} {2}", message.tags["display-name"], length, reason));
				bot.Whisper(message.channel, message.user, reason);
			}
		}
	}
}
