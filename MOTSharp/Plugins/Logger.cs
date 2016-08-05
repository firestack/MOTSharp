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
	class Logger : IPlugin
	{
		System.IO.StreamWriter fout;

		public Logger()
		{
			fout = new System.IO.StreamWriter("log.txt", true);
			fout.AutoFlush = true;
		}

		~Logger()
		{
			//fout.Close();
		}

		[Attributes.Command(Permissions.TMI, MsgAction.ALL, "")]
		public override void Execute()
		{
			fout.WriteLine(message.raw);

		}
	}
}
