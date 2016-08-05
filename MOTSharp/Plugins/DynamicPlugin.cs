using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Plugins;
using MOTSharp.Attributes;
using MOTSharp.Enums;
namespace MOTSharp
{
	public class DynamicPlugin
	{
		protected Bots.MaskOfTruth parent;

		public List<Tuple<IPlugin, Command, PluginEnabled>> invokeList = new List<Tuple<IPlugin, Command, PluginEnabled>>();

		public void Invoke(DataTypes.Message message)
		{
			
			invokeList.ForEach((plugin) => {
				
				var methodAttribute = plugin.Item2;
				
				if (methodAttribute.RespondsTo.HasFlag(message.action) && message.permission.CompareTo(methodAttribute.AccessLevel) >= 0)
				{
					if (message.message.StartsWith(methodAttribute.command) || methodAttribute.command == String.Empty)
					{
						DataTypes.GenericConfig config = null;
						switch (message.action.GetMessageScope())
						{
							case MsgType.USER:
							case MsgType.GLOBAL:
								config = parent.cfg.globalConfig;
								break;
							case MsgType.CHANNEL:
								config = parent.cfg.GetChannel(message.channel);
								break;
							default:
								return;
						}
						
						var cfg = config.GetPluginConfig(plugin.Item1);
						if (cfg.enabled)
						{
							if (cfg.data == null)
							{
								var configAttribute = plugin.Item1.GetType().GetCustomAttributes(typeof(Attributes.Config), true);
								if (configAttribute != null && configAttribute.Length >= 1)
								{
									var configInstance = configAttribute[0] as Config;
									cfg.data = Activator.CreateInstance(configInstance.configClass);
								}
								else
								{
									cfg.data = 0;
								}
							}
							plugin.Item1.Invoke(parent, cfg, message);
						}
						
					}

				}
			});

		}

		protected void FindPlugins()
		{
			foreach(Type plugin in Utils.GetEnumerableOfType<IPlugin>())
			{
				PluginEnabled pluginAttribute = plugin.GetCustomAttributes(typeof(PluginEnabled), true)[0] as PluginEnabled;
				if (pluginAttribute != null && pluginAttribute.enabled)
				{
					var inst = (IPlugin)Activator.CreateInstance(plugin);

					var methodAttribute = inst.GetType()
						.GetMethod("Execute")
						.GetCustomAttributes(typeof(Command), true)
						.ToList()
						.Find(
							(idx) => idx is Command)
					as Command;

					if (methodAttribute != null)
					{
						invokeList.Add(Tuple.Create(inst, methodAttribute, pluginAttribute));
					}                    
				}
			}
		}

	   public DynamicPlugin(Bots.MaskOfTruth parent)
		{
			this.parent = parent;
			FindPlugins();
		}
	}
}
