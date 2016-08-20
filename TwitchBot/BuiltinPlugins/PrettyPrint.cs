using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Message;

namespace TwitchBot.Plugins
{
	[Attributes.Command( accessLevel = Message.EPermissions.NONE, respondsTo = Message.ECommand.ALL)]
	[Attributes.PluginEnabled(true)]
	class PrettyPrint : Classes.Operator
	{

		public override void Init()
		{
			if(bot is Classes.Bot)
			{
				(bot as Classes.Bot).OnSend += Read;
			}
		}

		public void Read(string msg)
		{
			Console.WriteLine(">> " + msg);
		}

		public override void Invoke()
		{
			//var bgColor = Console.BackgroundColor;
			//var fgColor = Console.ForegroundColor;
			//
			//Console.ForegroundColor = message.action.GetConsoleColor();
			Console.Write("<< ");
			//Console.ForegroundColor = message.permission.GetConsoleColor();
			Console.WriteLine(message.ToString());
			//
			//Console.BackgroundColor = bgColor;
			//Console.ForegroundColor = fgColor;
		}

		public override bool CanExecute(Message.Message msg, string val)
		{
			return true;
		}
	}
}
