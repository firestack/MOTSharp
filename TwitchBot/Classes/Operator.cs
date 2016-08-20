using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Classes
{
	/// <summary>
	/// Base class for operating on messages recieved from IRC stream
	/// </summary>
	[Attributes.PluginEnabled(enabled = true, exposed = false)]
	public abstract class Operator
	{

		/// <summary>
		/// Enabled flag for operators
		/// </summary>
		public virtual bool enabled { get { return true; } set { } }

		/// <summary>
		/// The current bot that this plugin belongs too
		/// </summary>
		[NonSerialized]
		public BotBase bot;

		/// <summary>
		/// The current message being worked on by the bot
		/// </summary>
		[NonSerialized]
		public Message.Message message;

		/// <summary>
		/// Funciton invoked for each message which this plugin can execute on
		/// </summary>
		public abstract void Invoke();

		internal void Setup(BotBase bot)
		{
			this.bot = bot;

			Init();
		}

		/// <summary>
		/// Overridable function for initalization per plugin
		/// </summary>
		public virtual void Init()
		{

		}

		/// <summary>
		/// Checks if this plugin can execute
		/// </summary>
		/// <param name="msg">Message to test against</param>
		/// <param name="val">String to compare message with</param>
		/// <returns>If this plugin can execute</returns>
		public virtual bool CanExecute(Message.Message msg, string val)
		{
			return (val == null && enabled) ? true : msg.message.StartsWith(val);
		}

	}
}
