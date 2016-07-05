using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Bots
{
    public class MaskOfTruth : Bot
    {
        public static MaskOfTruth Bot;

        public List<string> SuperUsers = new List<string>();
        
        public delegate void Event();
        public Event Startup;
        public Event Shutdown;

        public delegate void MessageEvent(string Message);
        public MessageEvent OnSend;
        public MessageEvent OnReceive;

        public delegate void ParsedMessage(MOTSharp.DataTypes.Message M);
        public ParsedMessage OnMessage;

        protected DynamicPlugin pluginDispatch;

        public DataTypes.Config cfg;

        public MaskOfTruth(string server, int port, string config) : base(server, port, "", "")
        {
            cfg = new DataTypes.Config(config);
            cred = new DataTypes.Credentials(cfg.cfgFile.twitch.username, cfg.cfgFile.twitch.password);

            pluginDispatch = new DynamicPlugin(this);

            Bot = this;
            Startup += login;
            Startup += () => SuperUsers.ForEach((S) => S.ToLower());
            Shutdown += logout;
            Shutdown += () => SockStream.Dispose();
            Shutdown += () => SockConn.Close();

            OnSend += (string M) => Console.WriteLine(">> " + M);

            OnReceive += (string M) =>
            {
                var incomingMessage = new DataTypes.Message(this, M);
                if (incomingMessage.isVaild)
                {
                    OnMessage(incomingMessage);
                }
            };

            OnMessage += (DataTypes.Message M) => pluginDispatch.Invoke(M);
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
        
        public void stop()
        {
            // Do any database clean up here. 
            running = false;
        }

        public override void send(string message)
        {
            OnSend(message);
            base.send(message);
        }

        public void Join(string channel)
        {
            send(String.Format("JOIN #{0}", channel.Trim(new char[] { '#' })));
        }

        public void Leave(string channel)
        {
            send(String.Format("PART #{0}", channel.Trim(new char[] { '#' })));
        }

        public void PM(string channel, string message)
        {
           
            send(String.Format("PRIVMSG #{0} :{1}", channel.Trim(new char[] { '#' }), message.Replace("\r\n", "--")));
        } 

        public void Whisper(string channel, string user, string message)
        {
            PM(channel, String.Format("/w {0} {1}", user, message));
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
