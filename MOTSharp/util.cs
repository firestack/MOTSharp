using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MOTSharp.Enums;

namespace MOTSharp
{
	#region Util Classes
	public static class Utils
	{

		///<summary>
		/// This is probably an expensive function
		///</summary>
		public static IEnumerable<Type> GetEnumerableOfType<T>()
		{
			Type type = typeof(T);
			IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

			return types;
		}
	}

	#endregion
	#region Extention Methods
	public static class UriExtensions
	{
		public static string GetUsername(this Uri uri)
		{
			if (uri == null || string.IsNullOrWhiteSpace(uri.UserInfo))
				return string.Empty;

			var items = uri.UserInfo.Split(new[] { ':' });
			return items.Length > 0 ? items[0] : string.Empty;
		}

		public static string GetPassword(this Uri uri)
		{
			if (uri == null || string.IsNullOrWhiteSpace(uri.UserInfo))
				return string.Empty;

			var items = uri.UserInfo.Split(new[] { ':' });
			return items.Length > 1 ? items[1] : string.Empty;
		}

		public static string GetPathEnd(this Uri uri)
		{
			if (uri == null || string.IsNullOrWhiteSpace(uri.AbsolutePath))
			{
				return string.Empty;
			}
			return new string(uri.AbsolutePath.Skip(1).ToArray());
		}
	}

	public static class PGDBExtentions
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

	public static class EnumExtentions
	{
		public static MsgType GetMessageScope(this MsgAction act)
		{
			var ChannelFlags = MsgAction.PRIVMSG | MsgAction.USERSTATE | MsgAction.ROOMSTATE | MsgAction.CLEARCHAT | MsgAction.NOTICE;


			if (act.HasFlag(MsgAction.WHISPER))
			{
				return MsgType.USER;
			}
			else if (act.HasFlag(ChannelFlags))
			{
				return MsgType.CHANNEL;
			}
			else
			{
				return MsgType.GLOBAL;
			}
		}

		public static ConsoleColor GetConsoleColor(this MsgAction act)
		{
			ConsoleColor color = ConsoleColor.White;
			switch (act)
			{
				case MsgAction.RECONNECT:
				case MsgAction.USERNOTICE:
				case MsgAction.ROOMSTATE:
				case MsgAction.HOSTTARGET:
				case MsgAction.UNKNOWN:
					color = ConsoleColor.DarkYellow;
					break;

				case MsgAction.WHISPER:
				case MsgAction.PRIVMSG:
					color = ConsoleColor.Green;
					break;

				case MsgAction.PING:
					color = ConsoleColor.Gray;
					break;

				case MsgAction.CAP:
				case MsgAction.NOTICE:
				case MsgAction.CLEARCHAT:
					color = ConsoleColor.Yellow;
					break;

				case MsgAction.USERSTATE:
					break;

				case MsgAction.NUMERIC:
					color = ConsoleColor.Blue;
					break;

				default:
					break;
			}
			return color;
		}

		public static ConsoleColor GetConsoleColor(this Permissions perm)
		{
			ConsoleColor color = ConsoleColor.White;
			switch (perm)
			{
				case Permissions.NONE:
					color = ConsoleColor.DarkMagenta;
					break;
				case Permissions.TMI:
					color = ConsoleColor.Yellow;
					break;
				case Permissions.USER:
					color = ConsoleColor.Gray;
					break;
				case Permissions.MOD:
					color = ConsoleColor.Green;
					break;
				case Permissions.SUBSCRIBER:
					color = ConsoleColor.DarkBlue;
					break;
				case Permissions.BROADCASTER:
					color = ConsoleColor.DarkCyan;
					break;
				case Permissions.SUPERUSER:
					color = ConsoleColor.Cyan;
					break;
				default:
					color = ConsoleColor.Red;
					break;
			}
			return color;
		}
	}
	#endregion
}
