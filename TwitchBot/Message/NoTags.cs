using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
	public abstract class NoTags : Message
	{
		public override bool isUserMessage { get { return false; } }

		public override Dictionary<string, string> tags { get { return null; } }

		protected override List<string> body
		{
			get
			{
				if (bodyCache == null)
				{
					var cleanBody = new String(raw.Skip(1).ToArray());
					bodyCache = new List<string>();

					var bodySplit = cleanBody.Split(":".ToCharArray(), 2, StringSplitOptions.None);
					bodyCache.AddRange(bodySplit[0].Split(' '));
					bodyCache.Add(bodySplit.Length == 1 ? "" : bodySplit[1]);
				}
				return bodyCache;
			}
		}
	}
}
