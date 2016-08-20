using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.BuiltinPlugins
{
	[Attributes.Command(accessLevel = Message.EPermissions.TMI, respondsTo = Message.ECommand.JOIN)]
	public class OnJoin : Classes.Operator
	{
		public override void Invoke()
		{
			bot.channels.Add(message.command[1].TrimStart('#'));
			//Console.WriteLine("Joined Channel " + message.command[1]);
		}
	}
}
