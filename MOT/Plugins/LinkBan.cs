using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.DesignerServices;

using Newtonsoft.Json;
using RestSharp;

namespace MOT.Plugins
{
	
	[TwitchBot.Attributes.Command( accessLevel = TwitchBot.Message.EPermissions.TMI, respondsTo = TwitchBot.Message.ECommand.PRIVMSG)]
	class LinkBan : TwitchBot.Classes.Plugin
	{
		   
		private Regex expression = new Regex(@"(?:(https?:\/\/)?[\w\S]+\.[\w\S]+)");
		private RestClient client = new RestClient("https://api.twitch.tv/");
		private RestRequest baseRequest = new RestRequest("/kraken/users/{user}");

		public LinkBan()
		{
			baseRequest.AddHeader("client-id", "3oftkh3pwgk92ck0cmjb30d0axs2ac8");
		}

		public override void Invoke()
		{
			if (expression.IsMatch(message.message))
			{
				var userRequest = baseRequest.AddUrlSegment("user", message.tags["display-name"]);
				client.GetAsync(userRequest, (response, handle) => {
					var data = JsonConvert.DeserializeObject<JSON.user>(response.Content);
					if ((System.DateTime.Now - data.created_at).Days < 30)
					{
						bot.PM(message.channel, string.Format("/timeout {0} {1} {2}", message.user, 120, "Posted a link with an account younger than 30 days"));
					}
				});
				
			}
		}
	}
}
