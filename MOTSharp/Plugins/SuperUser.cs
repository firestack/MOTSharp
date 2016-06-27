using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class SuperUser : IPlugin
    {
        [Attributes.Command(Permissions.SUPERUSER, MsgAction.PRIVMSG, "^superuser add")]
        public override void Execute(Message message)
        {
            Bots.MaskOfTruth.Bot.PM(new string(message.actions[2].Skip(1).ToArray()), "Haha, No");
        }
    }
}
