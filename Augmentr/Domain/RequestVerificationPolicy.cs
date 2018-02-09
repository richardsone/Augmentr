using System;
using System.Linq;
using System.Net;
using Augmentr.Dal;
using Augmentr.Dal.Models;

namespace Augmentr.Domain
{
    public interface IRequestVerificationPolicy
    {
        bool VerifyRequest(string ip);
        void RecordBadRequest(string ip);
        void RecordValidRequest(string ip);
    }

    public class RequestVerificationPolicy : IRequestVerificationPolicy
    {
        private readonly DataContext _context;
        private const int MaxRequestsBeforeLockout = 5;

        public RequestVerificationPolicy(DataContext context)
        {
            _context = context;
        }

        public bool VerifyRequest(string ip)
        {
            var userAttempts = _context.Attempts.FirstOrDefault(_ => _.IP == ip);

            return userAttempts == null || userAttempts.Timeout < DateTime.Now;
        }

        public void RecordBadRequest(string ip)
        {
            var userAttempts = _context.Attempts.FirstOrDefault(_ => _.IP == ip);

            if (userAttempts == null)
            {
                CreateNewAttempt(ip);
            }
            else
            {
                UpdateAttempts(userAttempts);
            }

            _context.SaveChanges();
        }

        public void RecordValidRequest(string ip)
        {
            var userAttempts = _context.Attempts.FirstOrDefault(_ => _.IP == ip);

            if (userAttempts != null)
            {
                ResetAttempts(userAttempts);
            }

            _context.SaveChanges();
        }

        private void CreateNewAttempt(string ip)
        {
            var attempt = new UserAttempt
            {
                IP = ip,
                Attempts = 1
            };
            _context.Attempts.Add(attempt);
        }

        private void UpdateAttempts(UserAttempt userAttempts)
        {
            userAttempts.Attempts++;
            if (userAttempts.Attempts >= MaxRequestsBeforeLockout)
            {
                userAttempts.Timeout = DateTime.Now.AddHours(2);
            }
            _context.Attempts.Update(userAttempts);
        }

        private void ResetAttempts(UserAttempt userAttempts)
        {
            userAttempts.Attempts = 0;

            _context.Attempts.Update(userAttempts);
        }
    }
}