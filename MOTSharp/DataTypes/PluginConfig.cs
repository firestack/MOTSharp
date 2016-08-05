using System.Collections.Generic;


namespace MOTSharp.DataTypes
{
	public class PluginConfig
	{
		public string channelName = "";
		public int userId = 0;

		public bool enabled = true;

		// You better know what type that object data is
		public object data;
	}
}
