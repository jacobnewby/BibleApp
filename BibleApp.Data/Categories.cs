using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleApp.Data
{
    public class Categories
    {
        public Guid UserID { get; set; }
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }
    }
}
