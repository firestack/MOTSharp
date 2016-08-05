using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;

namespace MOTSharp.Plugins
{
	[Attributes.PluginEnabled(true, true)]
	class DBMessageCount : DataBaseQuery
	{
		protected override string QueryString(PluginConfig cfg, Message message)
		{
			return "SELECT COUNT(message) FROM messages";
		}
	}
}
