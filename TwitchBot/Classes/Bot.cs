using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Message;

namespace TwitchBot.Classes
{
	/// <summary>
	/// Base class for bots which interact with twitch
	/// </summary>
	public class Bot : BotBase
	{
		/// <summary>
		/// Event called when 
		/// </summary>
		/// <param name="sent"></param>
		public delegate void SendEvent(string sent);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		public delegate void RecvEvent(Message.Message msg);



		/// <summary>
		/// Event called when the bot sends data on the socket
		/// </summary>
		public event SendEvent OnSend;

		/// <summary>
		/// Evetn called when the bot receives data on the socket
		/// </summary>
		public event RecvEvent OnRecv;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public override void Send(string message)
		{
			OnSend?.Invoke(message);
			base.Send(message);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override Message.Message ReceiveMessage()
		{
			var msg = base.ReceiveMessage();
			OnRecv?.Invoke(msg);
			return msg;
		}
	}
}
