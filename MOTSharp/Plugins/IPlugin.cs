using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MOTSharp.Attributes;

namespace MOTSharp.Plugins
{
    [PluginEnabled(true)]
    public abstract class IPlugin
    {
        abstract public void Execute(Bots.MaskOfTruth bot, DataTypes.PluginConfig cfg, DataTypes.Message message);
    }
}
