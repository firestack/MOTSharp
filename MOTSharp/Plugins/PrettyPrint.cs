using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true)]
    class PrettyPrint : IPlugin
    {
        [Attributes.Command(Permissions.NONE, MsgAction.ALL, "")]
        public override void Execute(Message message)
        {
            switch (message.userPermissions)
            {
                case Permissions.NONE:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case Permissions.TMI:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Permissions.USER:
                    Console.ForegroundColor = ConsoleColor.Gray; 
                    break;
                case Permissions.MOD:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Permissions.BROADCASTER:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case Permissions.SUPERUSER:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine("<< {0}", message.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
