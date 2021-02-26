using System;

namespace NotSocialNetwork.Application.Exceptions
{
    public class ObjectNotFoundException : InvalidOperationException
    {
        public ObjectNotFoundException(string message) : base(message) { }
    }
}
