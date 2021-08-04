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
    public class CommentCreate
    {
        public int CommentId { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Commented By")]
        public string DisplayName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        [Required]
        [MaxLength(6000)]
        [Display(Name = "Content")]
        public string CommentContent { get; set; }
    }
}
