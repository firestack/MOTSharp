using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MOTSharp.Attributes;

namespace MOTSharp.Plugins
{
    [PluginEnabled(false)]
    class HelloWorld : IPlugin
    {
        public override void Execute(DataTypes.Message M)
        {
            if (M.message.Contains("Hello"))
            {
                Bots.MaskOfTruth.Bot.PM(M.actions[2], "Hiya!");
            }
        }
    }
}
