using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Enums;

namespace MOTSharp.DataTypes
{
    public class Config
    {
        protected Uri uri;

        protected Dictionary<string, ChannelConfig> channelConfig { get; private set; }
        public GlobalConfig globalConfig { get; private set; }

        internal Dictionary<string, string> TLData = new Dictionary<string, string>();
        internal JSON.configFile cfgFile;

        public Config() : this(@"Data/config.json") {}

        public Config(string configFile)
        {
            using (var fin = System.IO.File.OpenText(configFile))
            {
                cfgFile = Newtonsoft.Json.JsonConvert.DeserializeObject<JSON.configFile>(fin.ReadToEnd());
                uri = new Uri(cfgFile.motapi);
            }

            channelConfig = new Dictionary<string, ChannelConfig>();
            globalConfig = new GlobalConfig();
            Update();
        }

        public void Update()
        {

        }

        protected void UpdateInternal()
        {

        }

        protected void UpdateConfig(string cfgName)
        {

        }

        protected bool APIHasConfig(string cfgName, Action<bool> callback)
        {
            return false;
        }

        protected bool APICreateConfig(string cfgName, Action<bool> callback)
        {
            return false;
        }

        public ChannelConfig GetChannel(string channel)
        {
            ChannelConfig cfg;
            if (!channelConfig.TryGetValue(channel, out cfg))
            {
                cfg = channelConfig[channel] = new ChannelConfig();

            }
            return cfg;
        }
    }

    public abstract class GenericConfig
    {
        protected Dictionary<int, PluginConfig> KV = new Dictionary<int, PluginConfig>();

        public PluginConfig GetPluginConfig(Plugins.IPlugin plugin)
        {
            PluginConfig cfg;
            if(!KV.TryGetValue(plugin.GetType().GetHashCode(), out cfg))
            {
                cfg = KV[plugin.GetType().GetHashCode()] = new PluginConfig();
            }
            return cfg;
        }

    }

    public class GlobalConfig : GenericConfig
    {

    }

    public class ChannelConfig : GenericConfig
    {

    }
}
