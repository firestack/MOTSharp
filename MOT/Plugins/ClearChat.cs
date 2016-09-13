using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.Plugins
{
	[TwitchBot.Attributes.Command(accessLevel = TwitchBot.Message.EPermissions.TMI, respondsTo = TwitchBot.Message.ECommand.CLEARCHAT)]
	class ClearChat : TwitchBot.Classes.Plugin
	{
		public override void Invoke()
		{
			Console.WriteLine(message.raw);
		}
	}
}
