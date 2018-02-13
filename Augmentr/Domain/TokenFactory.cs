using System;
using Augmentr.Dal.Models;
using Augmentr.Domain.Models;
using JWT;
using Microsoft.Extensions.Options;

namespace Augmentr.Domain
{
    public interface ITokenFactory
    {
        string CreateTokenFromUser(User user);
        User CreateUserFromToken(string token, out bool expired);
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
            var userToken = MapUserToToken(user);

            var token = _encoder.Encode(userToken, _tokenSecret);

            return token;
        }

        public User CreateUserFromToken(string token, out bool expired)
        {
            var userToken = _decoder.DecodeToObject<UserToken>(token, _tokenSecret, false);

            expired = userToken.exp < DateTime.Now;

            var user = MapTokenToUser(userToken);

            return user;
        }

        private UserToken MapUserToToken(User user)
        {
            return new UserToken
            {
                Email = user.Email,
                Name = user.Name,
                exp = DateTime.Now.AddDays(2),
                Role = user.Role
            };
        }

        private User MapTokenToUser(UserToken userToken)
        {
            return new User
            {
                Email = userToken.Email,
                Name = userToken.Name,
                Role = userToken.Role
            };
        }
    }
}