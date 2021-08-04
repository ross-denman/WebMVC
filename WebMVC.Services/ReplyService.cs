using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class ReplyService
    {
        private readonly Guid _displayName;

        public ReplyService(Guid userId)
        {
            _displayName = userId;
        }
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();

        // Create Reply
        public bool CreateReply(ReplyCreate model)
        {
            Reply entity = new Reply()
            {
                ReplyId = model.ReplyId,
                DisplayName = _displayName.ToString(),
                CommentId = model.CommentId,
                ReplyContent = model.ReplyContent,
                CreatedUtc = DateTimeOffset.Now
            };
            ctx.Replies.Add(entity);
            return ctx.SaveChanges() == 1;
        }

        // Get Replies
        public IEnumerable<ReplyList> GetReplies()
        {
            var query = ctx.Replies
                .Select(e => new ReplyList
                {
                    ReplyId = e.ReplyId,
                    DisplayName = e.DisplayName,
                    CommentId = e.CommentId,
                    ReplyContent = e.ReplyContent,
                    CreatedUtc = e.CreatedUtc
                });
            return query.ToArray();
        }
        // Get Reply by Id
        public ReplyDetail GetReplyById(int id)
        {
            var entity = ctx.Replies.Single(
                e => e.ReplyId == id);

            return new ReplyDetail
            {
                ReplyId = entity.ReplyId,
                DisplayName = entity.DisplayName,
                CommentId = entity.CommentId,
                ReplyContent = entity.ReplyContent,
                CreatedUtc = entity.CreatedUtc,
                ModifiedUtc = entity.ModifiedUtc
            };
    }

        public bool UpdateReply(ReplyEdit model)
        {
            var entity = ctx.Replies.Single(
                e => e.ReplyId == model.ReplyId && e.ApplicationUser.Id == _displayName.ToString());

            entity.ReplyContent = model.ReplyContent;
            entity.ModifiedUtc = DateTimeOffset.UtcNow;

            return ctx.SaveChanges() == 1;
        }
        public bool DeleteReply(int ReplyId)
        {
            var entity = ctx.Replies.Single(
                e => e.ReplyId == ReplyId && e.ApplicationUser.Id == _displayName.ToString());

            ctx.Replies.Remove(entity);

            return ctx.SaveChanges() == 1;
        }
    }
}