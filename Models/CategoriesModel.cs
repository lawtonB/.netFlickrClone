using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlickrClone.Models
{
    
        [Table("Categories")]
        public class Category
        {
            [Key]
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public virtual ICollection<Picture> Pictures { get; set; }
        }
    
}
