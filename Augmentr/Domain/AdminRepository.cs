using System;
using System.Collections.Generic;
using System.Linq;
using Augmentr.Dal;
using Augmentr.Dal.Models;
using Augmentr.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Augmentr.Domain
{
    public interface IAdminRepository
    {
        IEnumerable<TagResponse> LoadAllTags(string token);
        void UpdateTag(TagRequest request);
        void DeleteTag(TagRequest request);
    }

    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        private readonly ITokenFactory _tokenFactory;

        public AdminRepository(DataContext context, ITokenFactory tokenFactory)
        {
            _context = context;
            _tokenFactory = tokenFactory;
        }

        public IEnumerable<TagResponse> LoadAllTags(string token)
        {
            if (TokenIsAdmin(token))
            {
                var tags = _context.Tags.ToList();

                var response = tags.Select(MapTagToResponse);

                return response;
            }
            return null;
        }

        // TODO: Update to return updated or original tag
        public void UpdateTag(TagRequest request)
        {
            if (TokenIsAdmin(request.Token))
            {
                // Create tag
                var tag = MapRequestToTag(request);

                _context.Tags.Update(tag);

                _context.SaveChanges();
            }
        }

        // TODO: Update to return deleted tag
        public void DeleteTag(TagRequest request)
        {
            if (TokenIsAdmin(request.Token))
            {
                // Create tag
                var tag = MapRequestToTag(request);

                _context.Tags.Remove(tag);

                _context.SaveChanges();
            }
        }

        private bool TokenIsAdmin(string token)
        {
            // try 
            // {
                // var user = _tokenFactory.CreateUserFromToken(token);

                // var isAdmin = user.Role >= Roles.Admin;

                return true;
            // }
            // catch (Exception)
            // {
            //     return false;
            // }
        }

        private Tag MapRequestToTag(TagRequest request)
        {
            return new Tag
            {
                Id = request.Id,
                Location = request.Location,
                Content = request.Content,
                TimePosted = request.TimePosted
            };
        }

        private TagResponse MapTagToResponse(Tag tag)
        {
            return new TagResponse
            {
                Id = tag.Id,
                UserEmail = tag.User.Email,
                Location = tag.Location,
                Content = tag.Content,
                TimePosted = tag.TimePosted
            };
        }
    }
}