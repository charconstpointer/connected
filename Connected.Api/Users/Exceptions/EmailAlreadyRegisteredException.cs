using System;

namespace Connected.Api.Users.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public EmailAlreadyRegisteredException(string message) : base(message)
        {
        }
    }
}