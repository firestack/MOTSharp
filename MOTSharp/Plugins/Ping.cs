using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MOTSharp.Bots;
using MOTSharp.DataTypes;
using MOTSharp.Enums;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class Ping : IPlugin
    {
        [Attributes.Command(Permissions.TMI, MsgAction.PING, "")]
        public override void Execute(MaskOfTruth bot, PluginConfig cfg, Message message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            bot.send(message.raw.Replace("PING", "PONG"));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
