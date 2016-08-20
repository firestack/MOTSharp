using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Plugins
{
	[Attributes.Command(accessLevel = Message.EPermissions.SUPERUSER, respondsTo = Message.ECommand.PRIVMSG, prefix = '>' ,suffix = "join")]
	[Attributes.PluginEnabled(true)]
	class JoinOperator : Classes.Operator
	{
		public override void Invoke()
		{
			foreach (string channel in message.message.Split(new char[] { ' ' }).Skip(1))
			{
				bot.Join(channel);
			}

		}
	}

}
