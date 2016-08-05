using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Attributes
{
    class Config : Attribute
    {
        public Type configClass;
        public Config(Type configClass)
        {
            this.configClass = configClass;
        }
    }
}
