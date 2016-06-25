using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Plugins
{
    class Printer : Plugin
    {
        public ResponseType commandType {  get { return ResponseType.send | ResponseType.recv; } }

        public override void Execute(string M)
        {
            Console.Write(M);
        }

        public Printer(MOTObject Parent) : base(Parent) {}
    }
}
