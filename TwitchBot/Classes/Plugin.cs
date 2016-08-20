using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Classes
{
	/// <summary>
	/// An operator already set up to be exposed to serialization and user interaction
	/// </summary>
	[Attributes.PluginEnabled(enabled = true, exposed = true)]
	public abstract class Plugin : Operator
	{ }
}
