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

        public string raw { get; private set; }
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
                    _parts = tmp.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
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

        protected Permissions? _userPermissions;
        public Permissions userPermissions
        {
            get
            {
                if (_userPermissions == null)
                {
                    if (isUserMessage)
                    {
                        if (Bots.MaskOfTruth.Bot.SuperUsers.Contains(tags["display-name"].ToLower())){
                            _userPermissions = Permissions.SUPERUSER;
                        }
                        else if (tags["display-name"].ToLower().Equals(new String(actions[2].Skip(1).ToArray()).ToLower()))
                        {
                            _userPermissions = Permissions.BROADCASTER;
                        }
                        else if (tags["user-type"].Equals("mod")){
                            _userPermissions = Permissions.MOD;
                        }
                        else{
                            _userPermissions = Permissions.USER;
                        }
                    }
                    else{
                        _userPermissions = Permissions.TMI;
                    }
                }

                return (Permissions)_userPermissions;
            }
        }

        protected MsgAction? _msgAction;
        public MsgAction msgAction
        {
            get
            {
                if (_msgAction == null)
                {

                    int tmpInt = 0;
                    MsgAction TVal;

                    if (isPing){
                        _msgAction = MsgAction.PING;
                    }
                    else if (int.TryParse(actions[1], out tmpInt))
                    {
                        _msgAction = MsgAction.NUMERIC;
                    }
                    else if (Enum.TryParse(actions[1], out TVal))
                    {
                        _msgAction = TVal;
                    }
                    else
                    {
                        _msgAction = MsgAction.UNKNOWN;
                    }
                }

                return (MsgAction)_msgAction;
            }
        }

        protected bool? _isUserMessage;
        public bool isUserMessage
        {
            get
            {
                if (_isUserMessage == null)
                {
                    _isUserMessage = hasTags && msgAction == MsgAction.PRIVMSG;
                }

                return (bool)_isUserMessage;
            }
        }

        protected bool? _isPing;
        public bool isPing
        {
            get
            {
                if(_isPing == null)
                {
                    _isPing = raw.StartsWith("PING");
                }
                return (bool)_isPing;
            }
        }

        protected string _channel;
        public string channel
        {
            get
            {
                if (_channel == null)
                {
                    _channel = new String(actions[2].Skip(1).ToArray());
                }
                return _channel;
            }
        }

        public Message(string raw)
        {
            this.raw = raw.Trim(new char[] { '\r', '\n' });
        }

        public bool isVaild
        {
            get
            {
                if (String.IsNullOrWhiteSpace(raw))
                {
                    return false;
                }

                return true;
            }
        }

        public override string ToString()
        {
            return this.raw;
        }
    }
}
