using System;
using System.Collections.Generic;
using System.Text;

namespace TheCuriousReaders.Services.CustomExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
