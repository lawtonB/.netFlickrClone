using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FlickrClone.Models
{
        [Table("Pictures")]
        public class Picture
        {
            [Key]
            public int PictureId { get; set; }
            public string PictureURL { get; set; }
            public int CategoryId { get; set; }
            public virtual Category Category {get; set;}
            public virtual ApplicationUser User { get; set; }

    }

}
