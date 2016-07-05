using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp
{
    public static class Utils
    {
        public static IEnumerable<Type> GetEnumerableOfInterface<T>()
        {
            var type = typeof(T);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract );

            return types;
        }

        public static IEnumerable<Type> GetEnumerableOfClass<T>()
        {
            var type = typeof(T);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

            return types;
        }
    }

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

        public  static string GetPathEnd(this Uri uri)
        {
            if (uri == null || string.IsNullOrWhiteSpace(uri.AbsolutePath))
            {
                return string.Empty;
            }
            return new string(uri.AbsolutePath.Skip(1).ToArray());
        }
    }
}
