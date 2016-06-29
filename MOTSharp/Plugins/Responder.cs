using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(false)]
    class Responder : IPlugin
    {
        [Attributes.Command(Permissions.TMI, MsgAction.CLEARCHAT | MsgAction.PRIVMSG, "")]
        public override void Execute(Message message)
        {
            Console.WriteLine("MESSAGE!! {0} {1}", message.msgAction.ToString(), message.message);
        }
    }
}
