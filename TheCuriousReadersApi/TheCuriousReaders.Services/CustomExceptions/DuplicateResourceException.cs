using System;
using System.Collections.Generic;
using System.Text;

namespace TheCuriousReaders.Services.CustomExceptions
{
    public class DuplicateResourceException : Exception
    {
        public DuplicateResourceException() : base() { }

        public DuplicateResourceException(string message) : base(message) { }
    }
}
