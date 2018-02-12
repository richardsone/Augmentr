using System.Linq;
using System.Collections.Generic;
using Augmentr.Dal;
using Augmentr.Dal.Models;
using Augmentr.Domain.Models;
using Microsoft.EntityFrameworkCore;
// using Encryption = BCrypt.Net.BCrypt;

namespace Augmentr.Domain
{
    public interface IUserRepository
    {
        string TryLogin(LoginRequest request);
        string TryRegister(RegisterRequest request);

        List<User> Query(string queryString);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ITokenFactory _tokenFactory;

        public UserRepository(DataContext context, ITokenFactory tokenFactory)
        {
            _context = context;
            _tokenFactory = tokenFactory;
        }

        public string TryLogin(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(_ => request.Email == _.Email && request.Password == _.Password);
            
            if(user!= null){
            return _tokenFactory.CreateTokenFromUser(user);
            } else {
                return null;
            }
            
        }

        public string TryRegister(RegisterRequest request)
        {
            var user = _context.Users.FirstOrDefault(_ => request.Email == _.Email);
            if(user != null)
            {
                return null;
            } else
            {
                user = MapRequestToUser(request);
                _context.Users.Add(user);
                _context.SaveChanges();
                return _tokenFactory.CreateTokenFromUser(user);
            }
        
        }

        private User MapRequestToUser(RegisterRequest request)
        {
            return new User
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Role = Roles.User
            };
        }

        public List<User> Query(string queryString)
        {
            var userResults = _context.Users.FromSql(queryString).ToList();
            return userResults;
        }
    }
}