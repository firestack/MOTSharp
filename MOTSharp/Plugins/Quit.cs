using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class Quit : IPlugin
    {
        [Attributes.Command(Permissions.SUPERUSER, MsgAction.PRIVMSG, ">Quit")]
        public override void Execute(Message message)
        {
            Bots.MaskOfTruth.Bot.stop();
        }
    }
}
