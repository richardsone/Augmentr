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
        void CreateTag(TagRequest request);
        void UpdateTag(TagRequest request);
        void DeleteTag(TagRequest request);
    }

    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public TagResponse LoadTag(int id)
        {
            var tag = _context.Tags
                .Include(_ => _.User)
                .FirstOrDefault(_ => _.Id == id);

            var response = MapTagToResponse(tag);

            return response;
        }

        public void CreateTag(TagRequest request)
        {
            // Verify token

            // Create tag
            var tag = MapRequestToTag(request, string.Empty);

            _context.Tags.Add(tag);

            _context.SaveChanges();
        }

        public void UpdateTag(TagRequest request)
        {
            // Verify token

            // Create tag
            var tag = MapRequestToTag(request, string.Empty);

            _context.Tags.Update(tag);

            _context.SaveChanges();
        }

        public void DeleteTag(TagRequest request)
        {
            // Verify token

            // Create tag
            var tag = MapRequestToTag(request, string.Empty);

            _context.Tags.Remove(tag);

            _context.SaveChanges();
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
    }
}