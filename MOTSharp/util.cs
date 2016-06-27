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
}
