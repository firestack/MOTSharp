using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class Range : Attribute
	{
		public int start;
		public int end;

		public Range(int start, int end)
		{
			this.start = start;
			this.end = end;
		}
	}
}
