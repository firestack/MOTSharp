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
    class AttrModifier : IPlugin
    {
        [Attributes.Command(Permissions.SUPERUSER, MsgAction.PRIVMSG, ">hello")]
        public override void Execute(MaskOfTruth bot, PluginConfig cfg, Message message)
        {
            Console.WriteLine(typeof(AttrModifier).ToString() + " called");

            var a = ((Attributes.Command)System.Reflection.MethodBase.GetCurrentMethod().GetCustomAttributes(typeof(Attributes.Command), true)[0]);

            Console.WriteLine(a.command);
            a.command = ">bye";
            Console.WriteLine(a.command);
            

        }
    }
}
