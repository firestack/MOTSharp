﻿using System;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace MOTSharp
{
	public class Bot
	{
		
		protected DataTypes.Credentials cred;

		// Incoming irc buffer
		protected string incoming = String.Empty;


		protected TcpClient SockConn;
		protected NetworkStream SockStream;
		protected bool running = true;
   

		public Bot(string server, int port, string nick, string pass) : this(nick, pass)
		{// Contect to twitch
			SockConn = new TcpClient(server, port);
			SockStream = SockConn.GetStream();             
		}

		protected Bot(string nick, string pass)
		{
			cred = new DataTypes.Credentials(nick, pass);
		}

		virtual public void send(string message)
		{
			if ( SockConn.Connected ){
				
				//OnSend(message);
				// I think this should be UTF8
				var data = Encoding.UTF8.GetBytes(message.TrimEnd(new char[] { '\r', '\n' }) + "\r\n");
				SockStream.Write(data, 0, data.Length);
			}
		}

		virtual protected string receive()
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

		virtual protected string receiveLine()
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
					incoming += receive();
					PartString();
				}
			}
			return bufferedLine;
		}

	}
}
