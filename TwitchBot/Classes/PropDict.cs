using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TwitchBot.Classes
{
    abstract public class PropDict : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void UpdateProperty([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        protected void UpdateProperty(params string[] names)
        {
            foreach(var name in names)
            {
                UpdateProperty(name);
            }
        }

		protected Dictionary<string, object> propertyValues = new Dictionary<string, object>();

		protected T SetProperty<T>(T value, [CallerMemberName] string property = null)
		{
			this.propertyValues[property] = value;
			UpdateProperty(property);
			return value;
		}

		protected object GetProperty([CallerMemberName] string property = null)
		{
			try
			{
				return this.propertyValues[property];
			}
			catch
			{
				return null;
			}		
		}
	}
}
