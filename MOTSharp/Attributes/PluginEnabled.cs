using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Attributes
{
    public class PluginEnabled : System.Attribute
    {
        public bool enabled = true;
        public PluginEnabled(bool enabled = true)
        {
            this.enabled = enabled;
        }
    }
}
