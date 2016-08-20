using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Plugins
{
	[Attributes.PluginEnabled(true)]
	[Attributes.Command( accessLevel = Message.EPermissions.SUPERUSER, respondsTo = Message.ECommand.PRIVMSG, prefix = '>', suffix = "leave")]
	class LeaveOperator : Classes.Operator
	{
		public override void Invoke()
		{
			foreach (string channel in message.message.Split(' ').Skip(1))
			{
				bot.Leave(channel);
			}
		}
	}
}
