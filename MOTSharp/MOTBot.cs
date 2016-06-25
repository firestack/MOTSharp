using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp
{
    public class MaskOfTruth : MOTBot
    {
        public static MaskOfTruth Bot;
        
        public delegate void Event();
        Event Startup;
        Event Shutdown;

        public delegate void MessageEvent(string Message);
        MessageEvent OnSend;
        MessageEvent OnReceive;

        public delegate void ParsedMessage(MOTSharp.DataTypes.Message M);
        ParsedMessage OnMessage;

        public MaskOfTruth(string server, int port, string nick, string pass) : base(server, port, nick, pass)
        {
            Bot = this;
            Startup += login;
            Shutdown += logout;
            Shutdown += () => SockStream.Dispose();
            Shutdown += () => SockConn.Close();

            OnSend += (string M) => Console.WriteLine(M);
            OnReceive += (string M) => Console.WriteLine(M);
            OnReceive += (string M) => OnMessage(new DataTypes.Message(M));
        }

        public void start()
        {
            Startup();
            while (running)
            {
                var dataString = receiveLine();
                OnReceive(dataString);
                
            }
            Shutdown();
        }

        public override void send(string message)
        {
            OnSend(message);
            base.send(message);
        }

        void login()
        {
            send(cred.PASS);
            send(cred.NICK);
        }

        void logout()
        {
            send("QUIT");
        }     
    }
}
