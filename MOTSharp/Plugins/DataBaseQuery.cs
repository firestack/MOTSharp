using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Bots;
using MOTSharp.DataTypes;
using MOTSharp.Enums;

using Npgsql;

namespace MOTSharp.Plugins
{

	[Attributes.PluginEnabled(false, false)]
	class DataBaseQuery : IPlugin
	{
		public NpgsqlConnection db;
		public NpgsqlCommand cmd = new NpgsqlCommand();
		public Uri pgurl;

		public DataBaseQuery()
		{
			db = PGDBExtentions.ConnectionFromURL(new Uri(@"postgres://postgres:mysecretpassword@107.170.251.210:32489/dev"));
			db.Open();
			cmd.Connection = db;
		}

		[Attributes.Command(Permissions.MOD, MsgAction.PRIVMSG, ">info")]
		public override void Execute()
		{

			cmd.CommandText = QueryString(cfg, message);

			var messageAction = new Task(async () => {
				var a = await ExecuteCmd();
				bot.PM(message.channel, Response(cfg, message, a));
			});

			messageAction.Start();     
		}

		protected virtual string QueryString(PluginConfig cfg, Message message)
		{

			return "";
		}

		protected virtual string Response(PluginConfig cfg, Message message, object dbData)
		{
			return dbData.ToString();
		}

		protected virtual async Task<object> ExecuteCmd()
		{
			return await cmd.ExecuteScalarAsync();
		}

		
	}
}
