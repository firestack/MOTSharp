﻿using MOTSharp.DataTypes;
using MOTSharp.Enums;

namespace MOTSharp.Attributes
{

    public class Command : System.Attribute
    {
        public Permissions AccessLevel;
        public MsgAction RespondsTo;
        public string command = "";

        public Command(Permissions AccessLevel, MsgAction RespondsTo, string command = "")
        {
            this.AccessLevel = AccessLevel;
            this.RespondsTo = RespondsTo;
            this.command = command;
        }

    }
}
