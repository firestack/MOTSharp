using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Classes
{
	public struct Credentials
	{
		/// <summary>
		/// Just For fun
		/// </summary>
		/// <param name="nickname"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public static Credentials New(string nickname, string password)
		{
			return new Credentials(nickname, password);
		}

		public Credentials(string nickname, string password)
		{
			this.nickname = nickname;
			this._password = "";
			this.password = password;
		}

		public string NICK { get { return String.Format("NICK {0}", nickname); } }
		public string PASS { get { return String.Format("PASS {0}", _password); } }
		public string nickname;
		private string _password;
		private string password {
			set
			{
				var tmp = value;
				if (!tmp.StartsWith("oauth:"))
				{
					tmp = "oauth:" + value;
				}
				_password = tmp;
			}
		}
	}
}
