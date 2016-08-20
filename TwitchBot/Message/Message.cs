using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
    // TODO: Add userid to message, TMI can be user 0
	/// <summary>
	/// 
	/// </summary>
	public abstract class Message : BaseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		protected ECommand? actionCache;
		/// <summary>
		/// 
		/// </summary>
		public override ECommand action
		{
			get
			{
				if (actionCache == null)
				{
					int tmpInt;
					ECommand TVal;
					if (int.TryParse(command.First(), out tmpInt))
					{
						actionCache = ECommand.NUMERIC;
					}
					else if (Enum.TryParse(command.First(), out TVal))
					{
						actionCache = TVal;
					}
					else
					{
						actionCache = ECommand.UNKNOWN;
					}
				}
				return (ECommand)actionCache;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected string channelCache;
		/// <summary>
		/// 
		/// </summary>
		public override string channel
		{
			get
			{
				if (channelCache == null)
				{
					channelCache = new String(command[1].Skip(1).ToArray());
				}
				return channelCache;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public override bool isValid { get { return !string.IsNullOrWhiteSpace(raw); } }

		/// <summary>
		/// 
		/// </summary>
		public override bool isUserMessage { get { return action == ECommand.PRIVMSG; } }

		/// <summary>
		/// 
		/// </summary>
		protected string prefixCache;
		/// <summary>
		/// 
		/// </summary>
		public override string prefix
		{
			get
			{
				if (prefixCache == null)
				{
					prefixCache = bodyCache[0];
				}
				return prefixCache;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected List<string> commandCache;
		/// <summary>
		/// 
		/// </summary>
		public override List<string> command
		{
			get
			{
				if (commandCache == null)
				{
					commandCache = body.GetRange(1, body.Count - 2);
				}
				return commandCache;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		protected string messageCache;
		/// <summary>
		/// 
		/// </summary>
		public override string message
		{
			get
			{
				if (messageCache == null)
				{
					messageCache = body.Last();
				}
				return messageCache;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected List<string> bodyCache;
		/// <summary>
		/// 
		/// </summary>
		protected virtual List<string> body { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return raw;
		}

	}
}
