using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp
{

    public enum ResponseType { recv, send };
    interface ICommand
    {
        ResponseType CommandType { get; }

        void Execute(string M);
        
    }
}
