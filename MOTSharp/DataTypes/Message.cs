using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Enums;

namespace MOTSharp.DataTypes
{
    public class AMessage
    {
        // https://github.com/ircv3/ircv3-specifications/blob/master/core/message-tags-3.2.md
        protected Bots.MaskOfTruth bot;
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
                        if (bot.SuperUsers.Contains(tags["display-name"].ToLower())){
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

        public AMessage(Bots.MaskOfTruth Bot, string raw)
        {
            this.bot = Bot;
            this.raw = raw.Trim('\r', '\n');
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
    
    public abstract class Message
    {
        public static Message ParseMessageString(Bots.MaskOfTruth bot, string msgStr)
        {
            // Parse Base Message
            Message msg = null;
            if (msgStr.StartsWith("@"))
            {
                // Has Tags
                msg = new TagsMessage();
            }
            else if(msgStr.StartsWith("PING"))
            {
                // Is Ping
                msg = new PingMessage();
            }
            else
            {
                // Doesn't Have Tags
                msg = new NoTagsMessage();
            }

            if(msg != null)
            {
                msg.Init(bot, msgStr);
            }          

            return msg;
        }

        public virtual void Init(Bots.MaskOfTruth bot, string raw)
        {
            this.bot = bot;
            this.raw = raw;
            this.raw = this.raw.TrimEnd('\r', '\n');
            time = DateTime.UtcNow;
        }
        // Info
        public DateTime time { get; private set; }
        protected Bots.MaskOfTruth bot;
        
        // Utility
        public virtual MsgAction action { get; }
        public virtual Permissions permission { get; }
        public virtual string channel { get; }
        public virtual string user { get; }
        public virtual bool isValid { get; }

        // Message Data
        public virtual Dictionary<string, string> tags { get; }
        public virtual string prefix { get; }
        public virtual List<string> command { get; }
        public virtual string message { get; }


        public string raw { get; protected set; }
    } 

    public abstract class BaseMessage : Message
    {
        protected MsgAction? actionCache;
        public override MsgAction action
        {
            get
            {
                if (actionCache == null)
                {
                    int tmpInt;
                    MsgAction TVal;
                    if (int.TryParse(command[1], out tmpInt))
                    {
                        actionCache = MsgAction.NUMERIC;
                    }
                    else if (Enum.TryParse(command[1], out TVal))
                    {
                        actionCache = TVal;
                    }
                    else
                    {
                        actionCache = MsgAction.UNKNOWN;
                    }
                }
                return (MsgAction)actionCache;
            }
        }

        protected string channelCache;
        public override string channel
        {
            get
            {
                if(channelCache == null)
                {
                    channelCache = new String(command[1].Skip(1).ToArray());
                }
                return channelCache;
            }
        }

        protected string prefixCache;
        public override string prefix
        {
            get
            {
                if (prefixCache == null)
                {
                    prefixCache = bodyCache[0];
                }
                return prefixCache;
            }
        }

        protected List<string> commandCache;
        public override List<string> command
        {
            get
            {
                if(commandCache == null)
                {
                    commandCache = body.GetRange(1, body.Count - 2);
                }
                return commandCache;
            }
        }

        protected string messageCache;
        public override string message
        {
            get
            {
                if(messageCache == null)
                {
                    messageCache = body.Last();
                }
                return messageCache;
            }
        }

        protected List<string> bodyCache;
        protected virtual List<string> body { get; }

    }

    public abstract class NoTags : BaseMessage
    {
        public override Dictionary<string, string> tags { get { return null; } }

        protected override List<string> body
        {
            get
            {
                if(bodyCache == null)
                {
                    var cleanBody = new String(raw.Skip(1).ToArray());
                    bodyCache = new List<string>();

                    var bodySplit = cleanBody.Split(":".ToCharArray(), 2, StringSplitOptions.None);
                    bodyCache.AddRange(bodySplit[0].Split(' '));
                    bodyCache.Add(bodySplit.Length == 1 ? "" : bodySplit[1]);
                }
                return bodyCache;
            }
        }
    }

    public abstract class Tags : BaseMessage
    {
        protected Dictionary<string, string> tagCache;
        public override Dictionary<string, string> tags
        {
            get
            {
                if (tagCache == null)
                {
                    tagCache = new Dictionary<string, string>();
                    var tagsString = new string(raw.Skip(1).ToArray());
                    foreach (var KV in tagsString.Split(' ')[0].Split(';'))
                    {
                        var keyvalue = KV.Split('=');
                        tagCache[keyvalue[0]] = keyvalue[1];
                    }

                }
                return tagCache;
            }
        }

        protected override List<string> body
        {
            get
            {
                if (bodyCache == null)
                {
                    // This gives the body of the message which isn't the tags
                    // @tags=true :user!user@user.name PRIVMSG #channel :message
                    //           ^ ^splitting there, returning this^^^^^^^^^^^^

                    var bodyArray = raw.Split(new string[] { " :" }, 3, StringSplitOptions.None);
                    bodyCache = new List<string>();

                    bodyCache.AddRange(bodyArray[2].Split(' '));
                    bodyCache.Add(bodyArray.Length == 2 ? "" : bodyArray[2]);
                }
                return bodyCache;
            }
        }

        public override string user
        {
            get
            {
                return tags["display-name"];
            }
        }
    }

    public class PingMessage : NoTags
    {
        public override MsgAction action { get { return MsgAction.PING; } }
        public override Permissions permission { get { return Permissions.TMI; } }
        public override string prefix { get { return "PING"; } }
        public override List<string> command { get { return (new string[] { "PING" }).ToList(); } }
        public override string message { get { return "tmi.twitch.tv"; } }
    }

    public class TagsMessage : Tags
    {
        protected Permissions? permissionCache;
        public override Permissions permission
        {
            get
            {
                if (permissionCache == null)
                {
                    if (bot != null && bot.SuperUsers.Contains(tags["display-name"].ToLower()))
                    {
                        permissionCache = Permissions.SUPERUSER;
                    }
                    else if (user.ToLower() == channel.ToLower())
                    {
                        permissionCache = Permissions.BROADCASTER;
                    }
                    else if (tags["user-type"].Equals("mod"))
                    {
                        permissionCache = Permissions.MOD;
                    }
                    else
                    {
                        permissionCache = Permissions.USER;
                    }
                }
                return (Permissions)permissionCache;
            }
        }
    }
    
    public class NoTagsMessage : NoTags
    {
        public override Permissions permission { get { return Permissions.TMI; } }
    }
}
