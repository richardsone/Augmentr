using System;
using System.Collections.Generic;
using System.Linq;
using Augmentr.Dal;
using Augmentr.Dal.Models;
using Augmentr.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Augmentr.Domain
{
    public interface ITagRepository
    {
        TagResponse LoadTag(int id);
        int CreateTag(TagRequest request);
        void UpdateTag(TagRequest request);
        void DeleteTag(TagRequest request);

        TagListResponse LoadTagForUser(string email);
    }

    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;
        private readonly ITokenFactory _tokenFactory;

        public TagRepository(DataContext context, ITokenFactory tokenFactory)
        {
            _context = context;
            _tokenFactory = tokenFactory;
        }

        public TagResponse LoadTag(int id)
        {
            var tag = _context.Tags
                .Include(_ => _.User)
                .FirstOrDefault(_ => _.Id == id);

            var response = MapTagToResponse(tag);

            return response;
        }

        public TagListResponse LoadTagForUser(string email)
        {
            var userWithTags = _context.Users.Include(_ => _.Tags).FirstOrDefault(_ => _.Email == email);
            return MapTagListToResponse(userWithTags);
        }

        public int CreateTag(TagRequest request)
        {
            // Verify token
            // If it deserializes correctly, create the tag
            var user = _tokenFactory.CreateUserFromToken(request.Token);

            // Create tag
            var tag = MapRequestToTag(request, user.Email);

            _context.Tags.Add(tag);

            _context.SaveChanges();

            return tag.Id;
        }

        // TODO: Update to return updated or original tag
        public void UpdateTag(TagRequest request)
        {
            // Verify token
            var user = _tokenFactory.CreateUserFromToken(request.Token);

            if (VerifyTokenMatchesPreviousTag(user, request))
            {
                // Create tag
                var tag = MapRequestToTag(request, user.Email);

                _context.Tags.Update(tag);

                _context.SaveChanges();
            }
        }

        // TODO: Update to return deleted tag
        public void DeleteTag(TagRequest request)
        {
            // Verify token
            var user = _tokenFactory.CreateUserFromToken(request.Token);

            if (VerifyTokenMatchesPreviousTag(user, request))
            {
                // Create tag
                var tag = MapRequestToTag(request, user.Email);

                _context.Tags.Remove(tag);

                _context.SaveChanges();
            }
        }

        private bool VerifyTokenMatchesPreviousTag(User user, TagRequest request)
        {
            var previousTag = _context.Tags
                .AsNoTracking()
                .Include(_ => _.User)
                .FirstOrDefault(_ => _.Id == request.Id);

            if (previousTag == null)
                throw new KeyNotFoundException(request.Id.ToString());

            return user.Email == previousTag.User.Email;
        }

        private Tag MapRequestToTag(TagRequest request, string userEmail)
        {
            return new Tag
            {
                Id = request.Id,
                UserEmail = userEmail,
                Location = request.Location,
                Content = request.Content,
                TimePosted = request.TimePosted
            };
        }

        private TagResponse MapTagToResponse(Tag tag)
        {
            return new TagResponse
            {
                User = new UserResponse
                {
                    Email = tag.User.Email,
                    Name = tag.User.Name
                },
                Location = tag.Location,
                Content = tag.Content,
                TimePosted = tag.TimePosted
            };
        }

        private TagListResponse MapTagListToResponse(User user)
        {
            var response = new TagListResponse
            {
                User = new UserResponse
                {
                    Email = user.Email,
                    Name = user.Name
                },
                tags = user.Tags.Select(MapTagToResponse).ToList()
            };

            return response;
        }
    }
}