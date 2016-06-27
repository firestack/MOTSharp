using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class LeaveOperator : IPlugin
    {
        [Attributes.Command(DataTypes.Permissions.SUPERUSER, DataTypes.MsgAction.PRIVMSG, "-leave")]
        public override void Execute(DataTypes.Message message)
        {
            foreach (string channel in message.message.Split(new char[] { ' ' }).Skip(1))
            {
                Bots.MaskOfTruth.Bot.Leave(channel);
            }
        }
    }
}
