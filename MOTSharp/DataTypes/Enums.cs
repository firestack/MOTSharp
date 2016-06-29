using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.DataTypes
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
    public enum MsgAction
    {//1111 1111
        UNKNOWN     = 1 << 0,
        PRIVMSG     = 1 << 1,
        PING        = 1 << 2,
        CLEARCHAT   = 1 << 3,
        USERSTATE   = 1 << 4,
        NUMERIC     = 1 << 5,
        NOTICE      = 1 << 6,
        CAP         = 1 << 7,
        
        ALL = ~0,
    };
}
