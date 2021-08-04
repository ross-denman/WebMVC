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
    public class CommentList
    {
        [Key]
        public int CommentId { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Commented By")]
        public string DisplayName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        [Display(Name = "Comment")]
        public string CommentContent { get; set; }
        [Display(Name = "Created (UTC)")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Last Modified (UTC)")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
