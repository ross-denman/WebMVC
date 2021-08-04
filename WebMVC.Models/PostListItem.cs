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
    public class PostListItem
    {
        [Key]
        public int PostId { get; set; }
        [Display(Name = "Posted By")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Title")]
        public string PostName { get; set; }
        // Add Later-  not sure of set up yet
        // 
        //public string ShortPost { get; set; }
        [Display(Name = "Created (UTC)")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified (UTC)")]
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
