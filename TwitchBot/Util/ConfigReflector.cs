using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Util
{
	public static class ConfigReflector
	{
		public static void GetConfigOfType(Type cls)
		{
			foreach (var prop in cls.GetProperties())
			{
				
			}

			foreach (var field in cls.GetFields())
			{
				var f = field;
				switch (Type.GetTypeCode(f.GetType()))
				{
					//case TypeCode.Empty:
					// case TypeCode.DBNull:
					case TypeCode.Object:

					case TypeCode.Boolean:
						
					case TypeCode.Byte:
					case TypeCode.UInt16:
					case TypeCode.UInt32:
					case TypeCode.UInt64:

					case TypeCode.SByte:
					case TypeCode.Int16:
					case TypeCode.Int32:
					case TypeCode.Int64:

					case TypeCode.Single:
					case TypeCode.Double:
					case TypeCode.Decimal:

					case TypeCode.DateTime:

					case TypeCode.Char:
					case TypeCode.String:

					default:
						break;
				}
			}
		}


	}
}
