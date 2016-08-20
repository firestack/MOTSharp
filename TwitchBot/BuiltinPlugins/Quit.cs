using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Plugins
{
	[Attributes.Command(accessLevel =Message.EPermissions.SUPERUSER, respondsTo =Message.ECommand.PRIVMSG, prefix ='>',suffix ="Quit")]
	[Attributes.PluginEnabled(true)]
	class Quit : Classes.Operator
	{
		public override void Invoke()
		{
			bot.running = false;
		}
	}
}
