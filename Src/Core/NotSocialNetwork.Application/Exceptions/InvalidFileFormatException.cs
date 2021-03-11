using System;

namespace NotSocialNetwork.Application.Exceptions
{
    public class InvalidFileFormatException : InvalidOperationException
    {
        public InvalidFileFormatException(string message) : base(message) { }
    }
}
