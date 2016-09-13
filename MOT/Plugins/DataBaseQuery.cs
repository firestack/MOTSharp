using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

namespace MOT.Plugins
{

	//[TwitchBot.Attributes.PluginEnabled(false, false)]
	[TwitchBot.Attributes.Command(accessLevel = TwitchBot.Message.EPermissions.MOD, respondsTo = TwitchBot.Message.ECommand.PRIVMSG, prefix = '>', suffix = "info")]
	public abstract class DataBaseQuery : TwitchBot.Classes.Plugin
	{
		[NonSerialized]
		public NpgsqlConnection db;

		[NonSerialized]
		public NpgsqlCommand cmd = new NpgsqlCommand();

		[NonSerialized]
		public Uri pgurl;

		public override void Init()
		{
			base.Init();
			db = Ext.PGBDExt.ConnectionFromURL(new Uri(@"postgres://postgres:mysecretpassword@107.170.251.210:32489/dev"));
			db.Open();
			cmd.Connection = db;
		}

		public override void Invoke()
		{

			cmd.CommandText = QueryString();

			var messageAction = new Task(async () => {
				var a = await ExecuteCmd();
				bot.PM(message.channel, Response(a));
			});

			messageAction.Start();     
		}

		protected virtual string QueryString()
		{

			return "";
		}

		protected virtual string Response(object dbData)
		{
			return dbData.ToString();
		}

		protected virtual async Task<object> ExecuteCmd()
		{
			return await cmd.ExecuteScalarAsync();
		}

		
	}
}
