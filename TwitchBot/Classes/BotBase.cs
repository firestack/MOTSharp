using System;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TwitchBot.Classes
{
	public abstract class BotBase
	{


		
		/// <summary>
		/// Incoming IRC Message Buffer
		/// </summary>
		protected string incoming = String.Empty;

		/// <summary>
		/// Socket Connection Object
		/// </summary>
		protected TcpClient SockConn;

		/// <summary>
		/// Socket Stream Object
		/// </summary>
		protected NetworkStream SockStream;


		public BotBase(string server, int port)
		{ 
			SockConn = new TcpClient(server, port);
			SockStream = SockConn.GetStream();
			
		}



		virtual public void Send(string message)
		{
			if (SockConn.Connected)
			{

				//OnSend(message);
				// I think this should be UTF8
				var data = Encoding.UTF8.GetBytes(message.TrimEnd('\r', '\n') + "\r\n");
				SockStream.Write(data, 0, data.Length);
			}
		}

		virtual protected string Receive()
		{
			if (SockConn.Connected)
			{
				// On _Data_ Received
				var data = new byte[2048];
				int bytes = SockStream.Read(data, 0, data.Length);
				return System.Text.Encoding.UTF8.GetString(data, 0, bytes);

			}
			else
			{
				return string.Empty;
			}
		}

		virtual protected string ReceiveLine()
		{
			string bufferedLine = string.Empty;

			// Utility string parser lambda(func)
			Action PartString = () =>
			{
				var temp = incoming.Split(new string[] { "\r\n" }, StringSplitOptions.None);
				if (temp.Length > 1)
				{
					bufferedLine = temp[0];
					incoming = string.Join("\r\n", temp.Skip(1));
				}
			};

			if (incoming.Contains("\r\n"))
			{
				PartString();
			}
			else
			{
				while (bufferedLine == string.Empty)
				{
					incoming += Receive();
					PartString();
				}
			}
			return bufferedLine;
		}

	}
}
