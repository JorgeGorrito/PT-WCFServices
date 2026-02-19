using System;

namespace PruebaTecnica.BDO.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public ValidationErrorException() : base() { }
        public ValidationErrorException(string message) : base(message: message) { }
    }
}
