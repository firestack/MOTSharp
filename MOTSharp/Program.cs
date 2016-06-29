using System;
using System.Collections.Generic;
using System.Linq;

namespace MOTSharp {
    class Program {
        static void Main(string[] args) {

            var MOT = new Bots.MaskOfTruth("irc.chat.twitch.tv", 80, "themaskoftruth", "oauth:");

            MOT.SuperUsers.AddRange(new string[] { "bomb_mask" });

            MOT.Startup += () => {
                MOT.send("CAP REQ :twitch.tv/tags");
                MOT.send("CAP REQ :twitch.tv/commands");
                MOT.send("JOIN #bomb_mask");
            };

            MOT.start();

            //End:
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadKey();
        }
    }
}
