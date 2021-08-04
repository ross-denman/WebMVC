using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Data;

namespace WebMVC.Models
{
    public class ReplyDetail
    {
        [Key]
        public int ReplyId { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Commented By")]
        public string DisplayName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public string ReplyContent { get; set; }
        [Display(Name = "Comment Date (UTC)")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Comment Modified (UTC)")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
