using System;

namespace Micro.Base.Exceptions
{
    public class MicroException : Exception
    {
        public string Code { get; }

        public MicroException()
        {

        }

        public MicroException(string codede)
        {
            Code = codede;
        }

        public MicroException(string message, params object[] args)
        : this(string.Empty, message, args)
        {

        }
        public MicroException(string code, string message, params object[] args)
: this(null, code, message, args)
        {

        }
        public MicroException(Exception exception, string message, params object[] args)
: this(exception, string.Empty, message, args)
        {

        }
        public MicroException(Exception exception, string code, string message, params object[] args)
: base(string.Format(message, args), exception)
        {
            Code = code;
        }

    }
}