using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Util
{
    /// <summary>
    /// 
    /// </summary>
	public static class EnumExt
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bf"></param>
        /// <returns></returns>
		public static IEnumerable<Enum> GetEnumsInBitFlag(this Enum bf)
		{
			foreach(Enum flag in Enum.GetValues(bf.GetType()))
			{
				if (bf.HasFlag(flag))
				{
					yield return flag;
				}
			}
		}
	}
}
