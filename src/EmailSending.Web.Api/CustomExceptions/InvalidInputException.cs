using System;

namespace EmailSending.Web.Api.CustomExceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}