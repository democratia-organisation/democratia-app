using com.democratia.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.democratia.CustomException
{
    internal class MailException : Exception
    {
        public MailException() : base() { }
    }

    internal class PassWordException : Exception
    {
        public PassWordException() : base() { }
    }
}
