﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MOT.Plugins
{
	[TwitchBot.Attributes.Command(accessLevel = TwitchBot.Message.EPermissions.SUPERUSER, prefix = '>', suffix = "info")]
	class DBMessageCount : DataBaseQuery
	{
		protected override string QueryString()
		{
			return "SELECT COUNT(message) FROM messages";
		}
	}
}
