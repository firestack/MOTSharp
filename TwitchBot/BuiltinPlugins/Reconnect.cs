using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.BuiltinPlugins
{
	[Attributes.PluginEnabled]
	[Attributes.Command(accessLevel = Message.EPermissions.TMI, respondsTo = Message.ECommand.RECONNECT | Message.ECommand.PRIVMSG, prefix = '>', suffix = "reconnect")]
	class Reconnect : Classes.Operator
	{
		public override void Invoke()
		{
			var channels = new HashSet<string>(bot.channels);
			bot.channels.Clear();
			bot.StopTimers();
			bot.Connect();
			bot.Login();
			bot.Join(channels);
			bot.StartTimers();
		}
	}
}
