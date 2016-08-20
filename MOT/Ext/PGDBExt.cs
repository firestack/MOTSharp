using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.Ext
{

	public static class PGBDExt
	{
		public static Npgsql.NpgsqlConnection ConnectionFromURL(Uri pgurl)
		{
			var db = new Npgsql.NpgsqlConnection(
				new Npgsql.NpgsqlConnectionStringBuilder(
					String.Format(
						"Host={0};Port={1};Database={2};Username={3};Password={4}",
						pgurl.Host, pgurl.Port, pgurl.GetPathEnd(), pgurl.GetUsername(), pgurl.GetPassword()
					)
				)
			);
			return db;
		}
	}
}
