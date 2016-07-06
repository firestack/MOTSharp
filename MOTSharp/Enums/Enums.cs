using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Enums
{
    public enum Permissions
    {
        NONE = 0,
        TMI = 1,
        USER = 2,
        MOD = 3,
        BROADCASTER = 4,
        SUPERUSER = 0xFF
    };

    [Flags]
    public enum MsgAction : int // 32 Bits of Flags!
    {
        UNKNOWN     = 1 << 0,
        PRIVMSG     = 1 << 1,
        PING        = 1 << 2,
        CLEARCHAT   = 1 << 3,
        USERSTATE   = 1 << 4,
        NUMERIC     = 1 << 5,
        NOTICE      = 1 << 6,
        CAP         = 1 << 7,
        WHISPER     = 1 << 8,
        HOSTTARGET  = 1 << 9,
        RECONNECT   = 1 << 10,
        USERNOTICE  = 1 << 11,
        ROOMSTATE   = 1 << 12,
        
        ALL = ~0,
    };

    public enum MsgType
    {
        GLOBAL,
        CHANNEL,
        USER,
    }

    public static class EnumExtentions
    {
        public static MsgType GetMessageScope(this MsgAction act)
        {
            var ChannelFlags = MsgAction.PRIVMSG | MsgAction.USERSTATE | MsgAction.ROOMSTATE | MsgAction.CLEARCHAT | MsgAction.NOTICE;


            if (act.HasFlag(MsgAction.WHISPER)) {
                return MsgType.USER;
            }
            else if (act.HasFlag(ChannelFlags))
            {
                return MsgType.CHANNEL;
            }
            else
            {
                return MsgType.GLOBAL;
            }            
        }
    }
}
