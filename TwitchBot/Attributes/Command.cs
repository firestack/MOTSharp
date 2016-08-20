

namespace TwitchBot.Attributes
{
	/// <summary>
	/// 
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class Command : System.Attribute
    {
		/// <summary>
		/// 
		/// </summary>
        public Message.EPermissions accessLevel = Message.EPermissions.MOD;

		/// <summary>
		/// 
		/// </summary>
        public Message.ECommand respondsTo = Message.ECommand.PRIVMSG;

		/// <summary>
		/// 
		/// </summary>
		public char prefix = '-';

		/// <summary>
		/// 
		/// </summary>
        public string suffix = null;

		/// <summary>
		/// 
		/// </summary>
        public string command { get { return suffix == null? null : prefix + suffix;  } }
		
    }
}
