using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Util
{
	public static class UriExt
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
}
