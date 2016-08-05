using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Bots;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
	
	class PluginLister : IPlugin
	{
		[Attributes.Command(Enums.Permissions.SUPERUSER, Enums.MsgAction.PRIVMSG, ">plugins")]
		public override void Execute()
		{
			string messageInfo = "The plugins in the system currently are: ";
			foreach (var info in bot.pluginDispatch.invokeList)
			{
				messageInfo += info.Item1.ToString() + " : ";
			}
			bot.PM(message.channel, messageInfo);
		}
	}
}
