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
	[Attributes.PluginEnabled(true, true)]
	class SuperUser : IPlugin
	{
		[Attributes.Command(Permissions.SUPERUSER, MsgAction.PRIVMSG, ">superuser add")]
		public override void Execute()
		{

			bot.PM(message.channel, "Haha, No");
		}
	}
}
