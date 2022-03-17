using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebgentalIdentity.Models
{
    public class Category
    {
        [Key]
        public int Cid { get; set; }

        [DisplayName("Category")]
        [Required]
        public string Cname { get; set; }
    }
}
