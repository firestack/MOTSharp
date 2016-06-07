using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.DataTypes
{
    public class Message
    {
        // https://github.com/ircv3/ircv3-specifications/blob/master/core/message-tags-3.2.md

        string raw;
        DateTime Time = DateTime.UtcNow;

        string _message;
        string message
        {
            get { return ""; }
        }

        Dictionary<String, String> tags { get { return null; } }

        string prefix { get; set; }

        List<string> command { get; set; }


        
        public Message(string raw)
        {
            this.raw = raw;
        }
    }
}
