using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Plugins
{
	// TODO: Fix Plugin Lister
	
	[Attributes.Command(accessLevel = Message.EPermissions.SUPERUSER, respondsTo = Message.ECommand.PRIVMSG, prefix = '>', suffix = "plugins")]
	class PluginLister : Classes.Operator
	{
		public override void Invoke()
		{
			//string messageInfo = "The plugins in the system currently are: ";
			//foreach (var info in bot.EH.invokeList)
			//{
			//	messageInfo += info.Item1.ToString() + " : ";
			//}
			//bot.PM(message.channel, messageInfo);
		}
	}
}
