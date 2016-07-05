using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RestSharp;

using MOTSharp.Bots;
using MOTSharp.DataTypes;
using MOTSharp.Enums;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled(true, true)]
    class LinkBan : IPlugin
    {
        private Regex expression = new Regex(@"(?:(https?:\/\/)?[\w\S]+\.[\w\S]+)");
        RestClient client = new RestClient("https://api.twitch.tv/");
        RestRequest baseRequest = new RestRequest("/kraken/users/{user}");

        public LinkBan()
        {
            baseRequest.AddHeader("client-id", "3oftkh3pwgk92ck0cmjb30d0axs2ac8");
        }

		[Attributes.Command(Permissions.TMI, MsgAction.PRIVMSG, "")]
        public override void Execute(MaskOfTruth bot, PluginConfig cfg, Message message)
        {
            if (expression.IsMatch(message.message))
            {
                var userRequest = baseRequest.AddUrlSegment("user", message.tags["display-name"]);
                client.GetAsync(userRequest, (response, handle) => {
                    var data = JsonConvert.DeserializeObject<JSON.user>(response.Content);
                    if ((System.DateTime.Now - data.created_at).Days < 30)
                    {
                        bot.PM(message.channel, string.Format("/timeout {0} {1} {2}", message.tags["display-name"], 120, "Posted a link with an account younger than 30 days"));
                    }
                });
            }
        }
    }
}
