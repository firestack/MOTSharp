

namespace TwitchBot.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class Command : System.Attribute
    {
        public Message.EPermissions accessLevel = Message.EPermissions.MOD;
        public Message.ECommand respondsTo = Message.ECommand.PRIVMSG;
		public char prefix = '-';
        public string suffix = null;

        public string command { get { return suffix == null? null : prefix + suffix;  } }
		
    }
}
