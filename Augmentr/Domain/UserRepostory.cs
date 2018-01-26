using System.Linq;
using Augmentr.Dal;
using Augmentr.Domain.Models;

namespace Augmentr.Domain
{
    public interface IUserRepository
    {
        string TryLogin(LoginRequest request);
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

            return string.Empty;
            // Tokenize it
        }
    }
}