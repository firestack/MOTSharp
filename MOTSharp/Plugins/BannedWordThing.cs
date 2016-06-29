using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled]
    class BannedWordThing : IPlugin
    {
        
        [Attributes.Command(Permissions.TMI, MsgAction.PRIVMSG, "")]
        public override void Execute(Message message)
        {
            var length = 1;
            var reason = "Reminder: K appa and it's variants are all banned here, they show up as *** for everyone else but yourself. if you use it you will get purged/timed out.";
            if (message.message.Contains("***"))
            {
                length =  Math.Max(Math.Min(length, 1209600), 1);

                Bots.MaskOfTruth.Bot.PM(message.channel, string.Format("/timeout {0} {1} {2}", message.tags["display-name"], length, reason));
                Bots.MaskOfTruth.Bot.PM(message.channel, string.Format("/w {0} {1}", message.tags["display-name"], reason)); 
            }
        }
    }
}
