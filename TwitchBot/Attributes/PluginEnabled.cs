using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PluginEnabled : System.Attribute
    {
        public bool enabled = true;
        public bool exposed = false;

        public PluginEnabled(bool enabled = true, bool exposed = false)
        {
            this.enabled = enabled;
            this.exposed = false;
        }
    }
}
