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
    [Attributes.PluginEnabled(true, true)]
    public class Wahey : IPlugin
    {
        DateTime? start;
        uint total = 0;
        System.Timers.Timer resetTimer;
        System.Timers.ElapsedEventHandler fin;

        public Wahey()
        {
            resetTimer = new System.Timers.Timer();
            resetTimer.Enabled = false;
            //setTimer.Elapsed +;
            resetTimer.Interval = 3000;            
        }

        private void OnFinish(Bot bot, Message msg)
        {
            if(bot is MaskOfTruth)
            {
                //"Chat had " + waheymessage + " messages containing Wahey and " + wahey + " individual Wahey's!" + " We also sent Waheys at " + ((double)wahey / (waheypersecondtimeend - waheypersecondtime)) + " per second!"
                ((MaskOfTruth)bot).PM(msg.channel, String.Format("Chat had {0} messages containing Wahey and 1 individual Wahey's, we also sent Waheys at {1} per second!", total, (double)total / (DateTime.Now - start).Value.Seconds));
            }

            total = 0;
            resetTimer.Enabled = false;
            start = null;

        }

        [Attributes.Command(Permissions.USER, MsgAction.PRIVMSG, "")]
        public override void Execute(MaskOfTruth bot, PluginConfig cfg, Message message)
        {
            if(fin == null)
            {
                //Lambda Function with reference to bot instance
                fin = (sender, args) => OnFinish(bot, message);
                resetTimer.Elapsed += fin;
            }

            var cmsg = message.message.ToLower();
            
            if (cmsg.Contains("wahey") || cmsg.Contains("wahay"))
            {
                if(start == null){ start = DateTime.Now; }
                total += 1;
                // This should reset the timer to start countdown again
                resetTimer.Stop();
                resetTimer.Start();
            }
        }


    }
}
