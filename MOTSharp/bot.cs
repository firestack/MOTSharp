using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace MOTSharp
{
    public class MOTBot : MOTObject
    {
        
        protected DataTypes.Credentials cred;

        // Incoming irc buffer
        protected string incoming = String.Empty;


        protected TcpClient SockConn;
        protected NetworkStream SockStream;
        protected bool turnover = true;
   
        public MOTBot(MOTObject Parent, string server, int port, string nick, string pass) : base(Parent)
        {
            
            cred = new DataTypes.Credentials(nick, pass);

            // Contect to twitch
            SockConn = new TcpClient(server, port);
            SockStream = SockConn.GetStream();             
        }

        void send(string message)
        {
            if ( SockConn.Connected ){
                
                //OnSend(message);
                // I think this should be UTF8
                var data = Encoding.ASCII.GetBytes(message.TrimEnd(new char[] { '\r', '\n' }) + "\r\n");
                SockStream.Write(data, 0, data.Length);
            }
        }

        string receive()
        {
            if (SockConn.Connected)
            {
                // On _Data_ Received
                var data = new Byte[2048];
                Int32 bytes = SockStream.Read(data, 0, data.Length);
                return System.Text.Encoding.UTF8.GetString(data, 0, bytes);

            }
            else
            {
                return String.Empty;
            }
        }

        string receiveLine()
        {
            String bufferedLine = String.Empty;

            // Utility string parser lambda(func)
            Action PartString = () =>
            {
                var temp = incoming.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (temp.Length > 1)
                {
                    bufferedLine = temp[0];
                    incoming = String.Join("\r\n", temp.Skip(1));
                }
            };

            if (incoming.Contains("\r\n"))
            {
                PartString();
            }
            else
            {
                while (bufferedLine == String.Empty)
                {
                    incoming += receive();
                    PartString();
                }
            }
            return bufferedLine;
        }
    }
}
