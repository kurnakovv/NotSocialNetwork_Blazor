using System;

namespace NotSocialNetwork.Application.Exceptions
{
    public class FavoritesNotFoundException : ArgumentNullException
    {
        public FavoritesNotFoundException(string message) : base(message) { }
    }
}
