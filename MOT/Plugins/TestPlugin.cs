using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.Plugins
{
	[TwitchBot.Attributes.Command(accessLevel = TwitchBot.Message.EPermissions.SUPERUSER, suffix = "test")]
	class TestPlugin : TwitchBot.Classes.Plugin
	{
		public override void Invoke()
		{
			var s = bot.EH.SerializeConfig();
		}
	}
}
