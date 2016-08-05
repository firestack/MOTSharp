using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
	public static class Helpers
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
}
