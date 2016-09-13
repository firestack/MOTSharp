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
		string s = string.Empty;

		public override void Invoke()
		{
			if(s == string.Empty)
			{
				//s = bot.EH.SerializeConfig();
			}
			else
			{
				//bot.EH.DeserializeConfig(s);
				s = "";
			}
			bot.PM(message.channel, "ก็็็็็็็็็็็็็ʕ•͡ᴥ•ʔ ก็็็็็็็็็็็็็ DIVE ก็็็็็็็็็็็็็ʕ•͡ᴥ•ʔ ก็็็็็็็็็็็็็");
		}
	}
}
