using System;

namespace MyExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found with the given ID.") { }

        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
