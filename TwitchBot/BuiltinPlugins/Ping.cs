using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Plugins
{
	[Attributes.Command(accessLevel =Message.EPermissions.TMI, respondsTo = Message.ECommand.PING)]
	[Attributes.PluginEnabled(true)]
	class Ping : Classes.Operator
	{
		public override void Invoke()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			bot.Send(message.raw.Replace("PING", "PONG"));
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
