using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class Ping : IPlugin
    {
        [Attributes.Command(Permissions.TMI, MsgAction.PING, "")]
        public override void Execute(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("Recieved PING!!! {0}", message.raw.Replace("PING", "PONG"));
            Bots.MaskOfTruth.Bot.send(message.raw.Replace("PING", "PONG"));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
