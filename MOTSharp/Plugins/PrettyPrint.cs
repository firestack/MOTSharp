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
	[Attributes.PluginEnabled(true)]
	class PrettyPrint : IPlugin
	{
		[Attributes.Command(Permissions.NONE, MsgAction.ALL, "")]
		public override void Execute()
		{
			var bgColor = Console.BackgroundColor;
			var fgColor = Console.ForegroundColor;
			//
			Console.ForegroundColor = message.action.GetConsoleColor();
			Console.Write("<< ");
			Console.ForegroundColor = message.permission.GetConsoleColor();
			Console.WriteLine(message.ToString());
			//
			Console.BackgroundColor = bgColor;
			Console.ForegroundColor = fgColor;
		}
	}
}
