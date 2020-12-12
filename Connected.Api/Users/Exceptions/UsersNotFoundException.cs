using System;

namespace Connected.Api.Users.Exceptions
{
    public class UsersNotFoundException : Exception
    {
        public UsersNotFoundException(string message) : base(message)
        {
        }
    }
}