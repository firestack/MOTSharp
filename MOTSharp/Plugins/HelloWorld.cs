using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Plugins
{
    class HelloWorld : Plugin
    {
        
        HelloWorld(MOTObject P) : base(P) { }


        public override void Execute(string M)
        {
            if (M.Contains("Hello"))
            {
                MaskOfTruth.Bot.send("PRIVMSG #bomb_mask :Hello!");
            }
        }
    }
}
