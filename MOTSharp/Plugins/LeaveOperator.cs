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
    class LeaveOperator : IPlugin
    {
        [Attributes.Command(Permissions.SUPERUSER, MsgAction.PRIVMSG, ">leave")]
        public override void Execute(MaskOfTruth bot, PluginConfig cfg, Message message)
        {
            foreach (string channel in message.message.Split(new char[] { ' ' }).Skip(1))
            {
                bot.Leave(channel);
            }
        }
    }
}
