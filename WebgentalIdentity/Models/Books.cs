using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebgentalIdentity.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookDetail { get; set; }
    }
}
