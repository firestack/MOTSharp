using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp
{
    [Flags]
    public enum ResponseType
    {
        recv = 0x1,
        send = 0x2,
    };

    interface ICommand
    {
        ResponseType CommandType { get; }

        void Execute(string M);
        
    }
}
