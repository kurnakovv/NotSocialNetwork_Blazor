using System;

namespace NotSocialNetwork.Application.Exceptions
{
    public class ObjectAlreadyExistException : InvalidOperationException
    {
        public ObjectAlreadyExistException(string message) : base(message) { }
    }
}
