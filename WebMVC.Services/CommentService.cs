using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CommentService
    {
        private readonly Guid _displayName;

        public CommentService(Guid userId)
        {
            _displayName = userId;
        }
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();

        // Create
        public bool CreateComment(CommentCreate model)
        {
            Comment entity = new Comment()
            {
                CommentId = model.CommentId,
                DisplayName = _displayName.ToString(),
                PostId = model.PostId,
                CommentContent = model.CommentContent,
                CreatedUtc = DateTimeOffset.Now
            };
            ctx.Comments.Add(entity);
            return ctx.SaveChanges() == 1;
        }

        // Get Comments
        public IEnumerable<CommentList> GetComments()
        {
            var query = ctx.Comments
                /*.Where(e => e.UserId == _displayName)*/
                .Select(e => new CommentList
                {
                    CommentId = e.CommentId,
                    DisplayName = e.DisplayName,
                    PostId = e.PostId,
                    CommentContent = e.CommentContent,
                    CreatedUtc = e.CreatedUtc
                });
            return query.ToArray();
        }
        // Get Comment by Id
        public CommentDetail GetCommentById(int id)
        {
            var entity = ctx.Comments.Single(
                e => e.CommentId == id);

            return new CommentDetail
            {
                CommentId = entity.CommentId,
                DisplayName = entity.DisplayName,
                PostId = entity.PostId,
                CommentContent = entity.CommentContent,
                CreatedUtc = entity.CreatedUtc,
                ModifiedUtc = entity.ModifiedUtc
            };
        }
        public bool UpdateComment(CommentEdit model)
        {
            var entity = ctx.Comments.Single(
                e => e.CommentId == model.CommentId && e.ApplicationUser.Id == _displayName.ToString());

            entity.CommentContent = model.CommentContent;
            entity.ModifiedUtc = DateTimeOffset.UtcNow;

            return ctx.SaveChanges() == 1;
        }
        public bool DeleteComment(int CommentId)
        {
            var entity = ctx.Comments.Single(
                e => e.CommentId == CommentId && e.ApplicationUser.Id == _displayName.ToString());

            ctx.Comments.Remove(entity);

            return ctx.SaveChanges() == 1;
        }
    }
}