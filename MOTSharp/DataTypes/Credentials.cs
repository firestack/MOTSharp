using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.DataTypes
{
	public struct Credentials
	{
		public Credentials(string nickname, string password)
		{
			this.nickname = nickname;
			this.password = password;
		}

		public string NICK { get { return String.Format("NICK {0}", nickname); } }
		public string PASS { get { return String.Format("PASS {0}", password); } }
		public string nickname;
		private string password;
	}
}
