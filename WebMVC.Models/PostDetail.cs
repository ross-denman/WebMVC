using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Data;

namespace WebMVC.Models
{
    public class PostDetail
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        [Display(Name = "Posted By")]
        public string DisplayName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        [MaxLength(160, ErrorMessage = "Please use 160 characters or less")]
        [Display(Name = "Title")]
        public string PostName { get; set; }
        [Display(Name = "Featured Image")]
        public string PostCoverImage { get; set; }
        [Required]
        [MaxLength(12000)]
        [Display(Name = "Content")]
        public string PostContent { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}