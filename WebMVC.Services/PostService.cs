using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Data;
using WebMVC.Models;
using Microsoft.AspNet.Identity;

namespace WebMVC.Services
{
    public class PostService
    {
        private readonly Guid _displayName;

        public PostService(Guid userId)
        {
            _displayName = userId;
        }
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();

        // Create
        public bool CreatePost(PostCreate model)
        {
            Post entity = new Post()
            {
                DisplayName = _displayName.ToString(),
                PostName = model.PostName,
                PostCoverImage = model.PostCoverImage,
                PostContent = model.PostContent,
                CreatedUtc = DateTimeOffset.Now
            };
            ctx.Posts.Add(entity);
            return ctx.SaveChanges() == 1;
        }

        // Get Posts
        public IEnumerable<PostListItem> GetPosts()
        {
            var query = ctx.Posts
                /*.Where(e => e.UserId == _displayName)*/
                .Select(e => new PostListItem
                {
                    PostId = e.PostId,
                    ApplicationUser = e.ApplicationUser, //check this as well as user id and try to get display name
                    PostName = e.PostName,
                    CreatedUtc = e.CreatedUtc
                });
            return query.ToArray();
        }
        // Get Posts Detail (ID)
        public PostDetail GetPostById(int id)
        {
            var entity = ctx.Posts.Single(
                e => e.PostId == id);

            return new PostDetail
            {
                PostId = entity.PostId,
                DisplayName = entity.DisplayName,
                PostName = entity.PostName,
                PostCoverImage = entity.PostCoverImage,
                PostContent = entity.PostContent,
                CreatedUtc = entity.CreatedUtc,
                ModifiedUtc = entity.ModifiedUtc
            };
        }
        public bool UpdatePost(PostEdit model)
        {
            var entity = ctx.Posts.Single(
                e => e.PostId == model.PostId && e.ApplicationUser.Id == _displayName.ToString());

            entity.PostName = model.PostName;
            entity.PostCoverImage = model.PostCoverImage;
            entity.PostContent = model.PostContent;
            entity.ModifiedUtc = DateTimeOffset.UtcNow;

            return ctx.SaveChanges() == 1;
        }
        public bool DeletePost(int postId)
        {
            var entity = ctx.Posts.Single(
                e => e.PostId == postId && e.ApplicationUser.Id == _displayName.ToString());

            ctx.Posts.Remove(entity);

            return ctx.SaveChanges() == 1;
        }
    }
}