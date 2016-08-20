using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
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
}
