using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MOTSharp.Bots;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class JoinOperator : IPlugin
    {
        [Attributes.Command(Enums.Permissions.SUPERUSER, Enums.MsgAction.PRIVMSG, ">join")]
        public override void Execute(MaskOfTruth bot, PluginConfig cfg, Message message)
        {
            foreach (string channel in message.message.Split(new char[] { ' ' }).Skip(1))
            {
                bot.Join(channel);
            }

        }
    }

}
