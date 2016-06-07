using System;

namespace MOTSharp {
    class Program {
        static void Main(string[] args){   
            var MyBot = new MOTBot(MOTObject.Global, "irc.chat.twitch.tv", 80, "justinfan007", "blaw");      

            Console.WriteLine("\n Press Enter to continue...");
            Console.ReadKey();
        }
    }
}
