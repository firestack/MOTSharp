using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Bots;
using MOTSharp.DataTypes;
using MOTSharp.Enums;

namespace MOTSharp.Plugins
{
	public class BannedWordData
	{
		
		[Newtonsoft.Json.JsonProperty("reason")]
		public string reason = "";

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

	[Attributes.Config(typeof(BannedWordData))]
	[Attributes.PluginEnabled(true, true)]
	class BannedWordThing : IPlugin
	{
		[Attributes.Command(Permissions.TMI, MsgAction.PRIVMSG, "")]
		public override void Execute()
		{
			if (message.message.StartsWith(">reason "))
			{
				(cfg.data as BannedWordData).reason = message.message;
			}
			//var length = 1;
			//var reason = "Reminder: K appa and it's variants are all banned here, they show up as *** for everyone else but yourself. if you use it you will get purged/timed out.";
			if (message.message.Contains("***"))
			{

				var a = cfg.data as BannedWordData;
				bot.PM(message.channel, string.Format("/timeout {0} {1} {2}", message.tags["display-name"], a.Length, a.reason));
				bot.PM(message.channel, string.Format("/w {0} {1}", message.tags["display-name"], a.reason));
			}
		}
	}
}
