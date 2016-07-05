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
        public bool exposedToUser = false;

        public PluginEnabled(bool enabled = true, bool exposed = false)
        {
            this.enabled = enabled;
            this.exposedToUser = false;
        }
    }
}
