using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class JoinOperator : IPlugin
    {
        [Attributes.Command(DataTypes.Permissions.SUPERUSER, DataTypes.MsgAction.PRIVMSG, "-join")]
        public override void Execute(DataTypes.Message M)
        {
            foreach (string channel in M.message.Split(new char[] { ' ' }).Skip(1))
            {
                Bots.MaskOfTruth.Bot.Join(channel);
            }

        }
    }

}
