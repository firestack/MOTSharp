using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp.Bots
{
    class TestBot : MaskOfTruth
    {
        System.IO.StreamReader fin;
        public TestBot(string server, int port, string config, string filename) : base(server, port, config)
        {
            fin = new System.IO.StreamReader(filename);
            OnReceive = (string M) =>
            {
                var incomingMessage = new DataTypes.Message(this, M);
                if (incomingMessage.isVaild)
                {
                    OnMessage(incomingMessage);
                }
            };
            OnSend = (S) => { };
        }

        public override void send(string message)
        {
            Console.WriteLine("::OFFLINE>> " + message);
        }

        protected string ReadFileLine()
        {
            var returnString = "";
            if (fin.EndOfStream)
            {
                running = false;
            }
            else
            {
                returnString = fin.ReadLine();
            }
            return returnString;
        }

        protected override string receive()
        {
            return ReadFileLine();
        }

        protected override string receiveLine()
        {
            return ReadFileLine();
        }
    }
}
