using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Plugins
{
    abstract class Plugin : MOTObject, ICommand
    {
        public ResponseType CommandType { get { return ResponseType.recv ; } }

        public abstract void Execute(string m);

        public Plugin(MOTObject Parent) : base(Parent) { }
    }
}
