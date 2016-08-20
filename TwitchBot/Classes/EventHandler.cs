using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwitchBot.Util;

namespace TwitchBot.Classes
{
	public class EventHandler
	{
		/// <summary>
		///  Simple Constructor
		/// </summary>
		public EventHandler()
		{

		}

		/// <summary>
		/// Parent
		/// </summary>
		public BotBase bot;

		/// <summary>
		/// Plugins in the current context
		/// </summary>
		public Dictionary<Message.ECommand, List<Tuple<Operator, Attributes.Command>>> plugins = new Dictionary<Message.ECommand, List<Tuple<Operator, Attributes.Command>>>();
		
		public void InvokePlugins(Message.Message msg)
		{
			foreach(var plugin in plugins[msg.action])
			{
				var methodAttribute = plugin.Item2;
				if (methodAttribute.respondsTo.HasFlag(msg.action) && msg.permission.CompareTo(methodAttribute.accessLevel) >= 0)
				{
					if(plugin.Item1.CanExecute(msg, methodAttribute.command))
					{
						plugin.Item1.message = msg;
						plugin.Item1.Invoke();
					}
				}
			}
		}

		public void LoadPlugins()
		{
			foreach(var op in Helpers.GetTypesInAssembly<Operator>())
			{
				var attr = op.GetCustomAttributes(typeof(Attributes.Command), true) as Attributes.Command[];
				var pe = op.GetCustomAttributes(typeof(Attributes.PluginEnabled), true) as Attributes.PluginEnabled[];

				if(pe.First().enabled == false)
				{
					continue;
				}

				if (attr != null)
				{
					Operator cache = null;
					foreach(Message.ECommand flag in attr.First().respondsTo.GetEnumsInBitFlag())
					{

						if(!plugins.ContainsKey(flag))
						{
							plugins[flag] = new List<Tuple<Operator, Attributes.Command>>();
						}
						if (cache == null)
						{
							cache = Activator.CreateInstance(op) as Operator;
						}
						
						plugins[flag].Add(Tuple.Create(cache, attr.First()));
						
					}

					cache.Setup(bot);
					cache = null;
				}

			}
		}
	}
}
