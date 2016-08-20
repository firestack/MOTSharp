using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwitchBot;
using TwitchBot.Message;
using TwitchBot.Util;

namespace MOT
{
	class Program
	{
		static void Main(string[] args)
		{
			var bot = new TwitchBot.Classes.Bot() { server = "irc.chat.twitch.tv", port = 80, superusers = new HashSet<string>() { "bomb_mask" } };
			bot.Start(TwitchBot.Classes.Credentials.New("TheMaskOfTruth", "5u7dxcgvtuqxgvds2pc1422cwqk8c8"));
		}
	}
}
