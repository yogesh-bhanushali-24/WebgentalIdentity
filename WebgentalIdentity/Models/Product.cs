using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebgentalIdentity.Models
{
    public class Product
    {
        [Key]
        public int Pid { get; set; }

        [DisplayName("Product Name")]
        [Required]
        public string Pname { get; set; }

        [DisplayName("Details")]
        [Required]
        public string Pdetail { get; set; }

        [DisplayName("Price")]
        [Required]
        public int? Pprice { get; set; }
        [DisplayName("Category")]
        [Required]
        public int? Cid { get; set; }
        [DisplayName("Image")]
        [Required]
        public string ProfileImage { get; set; }
        [DisplayName("Stock")]
        [Required]
        public int? Stock { get; set; }

        public ICollection<Cart> Carts { get; set; }
        public ICollection<Orders> Orderss { get; set; }
        public ICollection<DirectOrder> directOrders { get; set; }
    }
}
