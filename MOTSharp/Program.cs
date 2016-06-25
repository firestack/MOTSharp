using System;
using System.Collections.Generic;
using System.Linq;

namespace MOTSharp {
    class Program {
        static void Main(string[] args){


            new MaskOfTruth("irc.chat.twitch.tv", 80, "justinfan007", "blaw").start();
            

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadKey();
        }
    }
}
