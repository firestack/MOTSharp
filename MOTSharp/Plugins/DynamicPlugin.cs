using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Plugins;
using MOTSharp.Attributes;

namespace MOTSharp
{
    public class DynamicPlugin
    {

        protected List<IPlugin> invokeList = new List<IPlugin>();

        public void Invoke(DataTypes.Message message)
        {

            foreach (IPlugin plugin in invokeList)
            {

                var Attr = plugin.GetType().GetMethod("Execute").GetCustomAttributes(typeof(Command), true).ToList().Find((idx) => idx is Command ) as Command;
                if (Attr == null)
                {
                    continue;
                }
                
                if (Attr.RespondsTo.HasFlag(message.msgAction) && message.userPermissions.CompareTo(Attr.AccessLevel) >= 0)
                {
                    if (message.message.StartsWith(Attr.command) || Attr.command == String.Empty)
                    {
                        plugin.Execute(message);
                    }
                                     
                }
            }
        }

        protected void FindPlugins()
        {
            foreach(Type plugin in Utils.GetEnumerableOfClass<IPlugin>())
            {
                if(((PluginEnabled)plugin.GetCustomAttributes(typeof(PluginEnabled), true)?[0]).enabled)
                {
                    invokeList.Add((IPlugin)Activator.CreateInstance(plugin));
                }
            }
        }

       public DynamicPlugin()
        {
            FindPlugins();
        }


    }
}
