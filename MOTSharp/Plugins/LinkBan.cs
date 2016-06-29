using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.DataTypes;
using Newtonsoft.Json;
using RestSharp;

namespace MOTSharp.Plugins
{
    [Attributes.PluginEnabled]
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
        public override void Execute(Message message)
        {
            if (expression.IsMatch(message.message))
            {
                var userRequest = baseRequest.AddUrlSegment("user", message.tags["display-name"]);
                client.GetAsync(userRequest, (response, handle) => {
                    var data = JsonConvert.DeserializeObject<JSON.user>(response.Content);
                    if ((System.DateTime.Now - data.created_at).Days < 30)
                    {
                        Bots.MaskOfTruth.Bot.PM(message.channel, string.Format("/timeout {0} {1} {2}", message.tags["display-name"], 120, "Posted a link with an account younger than 30 days"));
                    }
                });
                //Bots.MaskOfTruth.Bot.PM(message.channel, string.Format("HEY @{0} THAT LOOKS LIKE A LINK. STOP", message.tags["display-name"]));
            }
        }
    }
}
