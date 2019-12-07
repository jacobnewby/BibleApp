using BibleApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleApp.Models
{
    public class VideoListItem
    {
        public Guid UserID { get; set; }
        [Key]
        public int VideoID { get; set; }
        [ForeignKey("Categories")]
        public int CategoryID { get; set; }
        public virtual Categories Categories { get; set; }
        public string VideoLink { get; set; }
    }
}
