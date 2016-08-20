using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{

	public class TagsMessage : Tags
	{
		protected EPermissions? permissionCache;
		public override EPermissions permission
		{
			get
			{
				if (permissionCache == null)
				{
					if (!isUserMessage)
					{
						permissionCache = EPermissions.TMI;
					}
					else if (bot != null && bot.superusers.Contains(user.ToLower()))
					{
						permissionCache = EPermissions.SUPERUSER;
					}
					else if (user.ToLower() == channel.ToLower())
					{
						permissionCache = EPermissions.BROADCASTER;
					}
					else if (tags["user-type"].Equals("mod"))
					{
						permissionCache = EPermissions.MOD;
					}
					//se if (tags["subscriber"])
					else
					{
						permissionCache = EPermissions.USER;
					}
				}
				return (EPermissions)permissionCache;
			}
		}

		public override string ToString()
		{
			if (action == ECommand.PRIVMSG)
				return "#" + channel + "::" + permission.ToString() + ":" + user + " :" + message;
			else
				return base.ToString();

		}
	}
}
