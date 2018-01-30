using System;
using Augmentr.Dal.Models;
using JWT;
using Microsoft.Extensions.Options;

namespace Augmentr.Domain
{
    public interface ITokenFactory
    {
        string CreateTokenFromUser(User user);
        User CreateUserFromToken(string token);
    }

    public class TokenFactory : ITokenFactory
    {
        private readonly IJwtEncoder _encoder;
        private readonly IJwtDecoder _decoder;
        private string _tokenSecret = "2548CBBADE864CC1AE017CFD4E6F255A";

        public TokenFactory(IJwtEncoder encoder, IJwtDecoder decoder)
        {
            _encoder = encoder;
            _decoder = decoder;
        }

        public string CreateTokenFromUser(User user)
        {
            var token = _encoder.Encode(user, _tokenSecret);

            return token;
        }

        public User CreateUserFromToken(string token)
        {
            var user = _decoder.DecodeToObject<User>(token, _tokenSecret, true);

            return user;
        }
    }
}