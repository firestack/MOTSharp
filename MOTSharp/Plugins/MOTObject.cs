using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTSharp
{
    public abstract class MOTObject
    {
        public static MOTObject Global;

        MOTObject _parent;
        MOTObject Parent {
            get { return _parent; }
            set { _parent = value; _parent?.AddChild(this); }
        }

        public MOTObject(MOTObject Parent)
        {
            this.Parent = Parent;
        }

        List<MOTObject> Children = new List<MOTObject>();
        
        public void AddChild(MOTObject Child)
        {
            if (!Children.Contains(Child) && Child.Parent == this)
            {
                Children.Add(Child);
            }            
        }
    }
}
