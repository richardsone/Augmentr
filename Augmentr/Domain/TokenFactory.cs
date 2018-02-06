using System;
using Augmentr.Dal.Models;
using JWT;
using JWT.Serializers;
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

        public TokenFactory(IJwtEncoder encoder, IJwtDecoder decoder)
        {
            _encoder = encoder;
            _decoder = decoder;
        }

        public string CreateTokenFromUser(User user)
        {
            var token = new JsonNetSerializer().Serialize(user);

            return token;
        }

        public User CreateUserFromToken(string token)
        {
            var user = new JsonNetSerializer().Deserialize<User>(token);

            return user;
        }
    }
}