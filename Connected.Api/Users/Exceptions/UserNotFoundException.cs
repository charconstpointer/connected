using System;
using System.Linq;

namespace Connected.Api.Users.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}