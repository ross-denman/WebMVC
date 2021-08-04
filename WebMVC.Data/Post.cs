using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Data
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Posted By")]
        public string DisplayName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        [MaxLength(160, ErrorMessage = "Please use 160 characters or less")]
        [Display(Name = "Title")]
        public string PostName { get; set; }
        public string PostCoverImage { get; set; }
        [Required]
        public string PostContent { get; set; }
        [Required]
        [Display(Name = "Created (UTC)")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Last Modified (UTC)")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}
