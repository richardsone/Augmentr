using System.Linq;
using Augmentr.Dal;
using Augmentr.Domain.Models;

namespace Augmentr.Domain
{
    public interface IUserRepository
    {
        string TryLogin(LoginRequest request);
        string TryRegister(RegisterRequest request);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public string TryLogin(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(_ => request.Email == _.Email && request.Password == _.Password);
            if(user!= null){
            return user.tokenize();
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
                user = new Dal.Models.User(request.Email, request.Password);
                _context.Users.Add(user);
                return user.tokenize();
            }
        
        }
    }
}