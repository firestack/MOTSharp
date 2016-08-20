using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace TwitchBot.Classes
{
	public abstract class BotBase : MessageSocket
	{


		/// <summary>
		/// Simple Constructor
		/// </summary>
		public BotBase()
		{
			slowMessage = new System.Timers.Timer() { Interval = slowLength, AutoReset = true };
			slowMessage.Elapsed += (sender, args) => MessageBalancer(Message.EPermissions.USER);

			fastMessage = new System.Timers.Timer() { Interval = fastLength, AutoReset = true };
			fastMessage.Elapsed += (sender, args) => MessageBalancer(Message.EPermissions.MOD);

			OnStartup += (sender) => { slowMessage.Start(); fastMessage.Start(); };

			EH = new EventHandler() { bot = this };
			EH.LoadPlugins();			
		}

		/// <summary>
		/// Basic Bot Event 
		/// </summary>
		public delegate void BotEvent(BotBase sender);

		/// <summary>
		/// Event called when the bot begins startup
		/// </summary>
		public event BotEvent OnStartup;

		/// <summary>
		/// Event called when the bot begins shutdown
		/// </summary>
		public event BotEvent OnShutdown;

		/// <summary>
		/// list of users with extrodinary powers
		/// </summary>
		public HashSet<string> superusers = new HashSet<string>();

		/// <summary>
		/// Length of time between messages on the slow buffer
		/// </summary>
		public float slowLength = 1.7f;

		/// <summary>
		/// Length of time between messages on the fast buffer
		/// </summary>
		public float fastLength = 0.07f;

		/// <summary>
		/// Non Mod Channel Timer
		/// </summary>
		protected System.Timers.Timer slowMessage;

		/// <summary>
		/// Mod Channel Timer
		/// </summary>
		protected System.Timers.Timer fastMessage;

		/// <summary>
		/// Message Buffer for non mod channels
		/// </summary>
		protected Queue<string> slowMessageBuffer = new Queue<string>();

		/// <summary>
		/// Message Buffer for modded channels
		/// </summary>
		protected Queue<string> fastMessageBuffer = new Queue<string>();

		/// <summary>
		/// Variable of whether to keep reading from stream
		/// </summary>
		public bool running
		{
			get
			{
				return _running;
			}
			set
			{
				_running = value;
			}
		}

		/// <summary>
		/// Behind the scenes running variable
		/// </summary>
		protected bool _running = true;

		/// <summary>
		/// Event Handler
		/// </summary>
		public EventHandler EH;

		/// <summary>
		/// Get messages from stream
		/// </summary>
		protected virtual IEnumerable<Message.Message> messages
		{
			get
			{
				while (running)
				{
					yield return ReceiveMessage();
				}
			}
		}

		/// <summary>
		/// Currently Joined Channels
		/// </summary>
		public HashSet<string> channels = new HashSet<string>();

		/// <summary>
		/// User credentials
		/// </summary>
		protected Credentials cred;

		/// <summary>
		/// Message buffer for rate limiting sending to twitch
		/// </summary>
		protected Queue<string> MessageBuffer(object state)
		{
			if (state is Message.EPermissions)
			{
				var s = state as Message.EPermissions?;
				if(s == Message.EPermissions.MOD)
				{
					return fastMessageBuffer;
				}
				else
				{
					return slowMessageBuffer;
				}
			}
			return null;
		}

		/// <summary>
		/// Read message object from stream
		/// </summary>
		/// <returns>Message Object from IRC</returns>
		protected virtual Message.Message ReceiveMessage()
		{
			return Message.Message.ParseMessageString(this,  ReceiveLine());
		}

		/// <summary>
		/// Called when channels is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ChannelUpdate(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			Action joinChannels = () => {
				foreach (string channel in e.NewItems)
				{
					Join(channel);
				}
			};
			Action leaveChannels = () => {
				foreach (string channel in e.OldItems)
				{
					Leave(channel);
				}
			};
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
					leaveChannels();
					joinChannels();
					break;

				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					leaveChannels();
				break;

				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					joinChannels();
					break;

				case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
				default:
					break;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		protected virtual void MessageBalancer(object state)
		{
			var buff = MessageBuffer(state);
			if(buff.Count > 0)
			{
				Send(buff.Dequeue());
			}
		}

		/// <summary>
		/// Join channels
		/// </summary>
		/// <param name="channels">Join this list of channels</param>
		public void Join(IEnumerable<string> channels)
		{
			foreach (var channel in channels)
			{
				Send(String.Format("JOIN #{0}", channel.Trim('#')));
			}
		}

		/// <summary>
		/// Syntaxic Sugar
		/// </summary>
		/// <param name="channels">List of channels to join</param>
		public void Join(params string[] channels)
		{
			Join(channels.ToList());
		}

		/// <summary>
		/// Leave this list of channels
		/// </summary>
		/// <param name="channels">list of channels</param>
		public void Leave(params string[] channels)
		{
			Leave(channels.ToList());
		}

		/// <summary>
		/// Leave this list of channels
		/// </summary>
		/// <param name="channels">List of channels</param>
		public void Leave(IEnumerable<string> channels)
		{
			foreach (var channel in channels)
			{
				Send(String.Format("PART #{0}", channel.Trim('#')));
			}
		}

		/// <summary>
		/// Send a message to a channel
		/// </summary>
		/// <param name="channel">target channel</param>
		/// <param name="message">message to send</param>
		public void PM(string channel, string message)
		{
			slowMessageBuffer.Enqueue(String.Format("PRIVMSG #{0} :{1}", channel.Trim('#'), message.Replace("\r\n", "--")));
		}

		/// <summary>
		/// Whisper User with message
		/// </summary>
		/// <param name="channel">Channel to communicate on</param>
		/// <param name="user">User to send message to</param>
		/// <param name="message">Message to send to user</param>
		public void Whisper(string channel, string user, string message)
		{
			PM(channel, String.Format("/w {0} {1}", user, message));
		}

		/// <summary>
		/// Request tags 
		/// </summary>
		/// <param name="tags"></param>
		public void RequestTags(IEnumerable<string> tags)
		{
			foreach (string tag in tags)
			{
				Send(String.Format("CAP REQ :{0}", tag));
			}
		}

		/// <summary>
		/// Syntaxic Sugar
		/// </summary>
		/// <param name="tags"></param>
		public void RequestTags(params string[] tags)
		{
			RequestTags(tags.ToList());
		}

		/// <summary>
		/// 
		/// </summary>
		public override void Disconnect()
		{
			if(sockConn != null)
			{
				Logout();
				base.Disconnect();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="joinChannel"></param>
		public void Login(bool joinChannel = false)
		{
			Send(cred.PASS);
			Send(cred.NICK);

			if (joinChannel)
			{
				Join(cred.nickname);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Logout()
		{
			Send("QUIT");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cred"></param>
		public virtual void Start(Credentials cred)
		{
			OnStartup?.Invoke(this);

			Connect();

			this.cred = cred;

			Login(true);
			Join(superusers);
			RequestTags("twitch.tv/tags", "twitch.tv/commands");

			foreach (var msg in messages)
			{
				EH.InvokePlugins(msg);
			}

			Disconnect();

			OnShutdown?.Invoke(this);
		}

		public virtual void StartTimers()
		{
			fastMessage.Start();
			slowMessage.Start();
		}

		public virtual void StopTimers()
		{
			fastMessage.Stop();
			slowMessage.Stop();
		}
	}
}
