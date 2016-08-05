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
		public virtual void InitalizeConfig(DataTypes.PluginConfig plg) { }

		protected Bots.MaskOfTruth bot;
		protected DataTypes.PluginConfig cfg;
		protected DataTypes.Message message;
		public virtual void Invoke(Bots.MaskOfTruth bot, DataTypes.PluginConfig cfg, DataTypes.Message message)
		{
			this.bot = bot;
			this.cfg = cfg;
			this.message = message;
			Execute();
			this.message = null;
		}

		abstract public void Execute();

		
	}
}
