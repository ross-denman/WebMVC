using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class CommentEdit
    {
        public int CommentId { get; set; }
        [MaxLength(6000)]
        [Display(Name = "Content")]
        public string CommentContent { get; set; }
    }
}
