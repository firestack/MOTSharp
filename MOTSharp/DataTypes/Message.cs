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

        protected Dictionary<string, string> _tags;
        public Dictionary<string, string> tags
        {
            get
            {
                if (_tags == null && hasTags)
                {
                    _tags = new Dictionary<string, string>();

                    foreach (var KV in raw.Split(new char[] { ' ' })[0].Split(new char[] { ';' }))
                    {
                        var keyvalue = KV.Split(new char[] { '=' });
                        _tags[keyvalue[0]] = keyvalue[1];
                    }
                    
                }
                return _tags;
            }
        }

        string prefix { get; set; }

        protected string[] _actions;
        public string[] actions
        {
            get
            {
                if (_actions == null)
                {
                    _actions = messageParts[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }
                return _actions;
            }
        }
        
        protected string _message;
        public string message
        {
            get
            {
                if (_message == null)
                {
                    _message = messageParts.Length >= 2 ? messageParts[1] : "";
                }
                return _message;
            }
        }

        protected string[] _parts;
        protected string[] messageParts
        {
            get
            {
                if (_parts == null)
                {
                    var tmp = (hasTags ? raw.Split(new char[] { ' ' }, 2)[1] : raw);
                    _parts = tmp.Split(new char[] { ':' }, 3, StringSplitOptions.RemoveEmptyEntries);
                }
                return _parts;
            }
        }

        protected bool? _hasTags;
        public bool hasTags
        {
            get
            {
                if (_hasTags == null)
                {
                    _hasTags = raw[0] == '@';
                }
                return (bool)_hasTags;
            }
        }

        public Message(string raw)
        {
            this.raw = raw.Trim(new char[] { '\r', '\n' });
        }

        public override string ToString()
        {
            return this.raw;
        }
    }
}
