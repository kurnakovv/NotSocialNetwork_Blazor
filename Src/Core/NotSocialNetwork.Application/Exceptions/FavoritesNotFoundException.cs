using System;

namespace NotSocialNetwork.Application.Exceptions
{
    public class FavoritesNotFoundException : InvalidOperationException
    {
        public FavoritesNotFoundException(string message) : base(message) { }
    }
}
