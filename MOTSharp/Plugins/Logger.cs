using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(false)]
    class Logger : IPlugin
    {
        System.IO.StreamWriter fout;

        public Logger()
        {
            fout = new System.IO.StreamWriter("log.txt", true);
            fout.AutoFlush = true;
        }

        ~Logger()
        {
            //fout.Close();
        }

        [Attributes.Command(Permissions.TMI, MsgAction.ALL, "")]
        public override void Execute(Message message)
        {
            fout.WriteLine(message.raw);

        }
    }
}
