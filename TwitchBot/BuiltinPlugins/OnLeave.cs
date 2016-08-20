using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.BuiltinPlugins
{
	[Attributes.Command(accessLevel = Message.EPermissions.TMI, respondsTo = Message.ECommand.LEAVE)]
	public class OnLeave : Classes.Operator
	{
		public override void Invoke()
		{
			bot.channels.Remove(message.command[1].TrimStart('#'));
			//Console.WriteLine("Left Channel " + message.command[1]);
		}
	}
}
