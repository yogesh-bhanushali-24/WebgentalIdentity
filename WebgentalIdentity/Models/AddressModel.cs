using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebgentalIdentity.Models
{
    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Pincode { get; set; }
        [Required]
        public string Address { get; set; }
        public string Landmark { get; set; }
        [Required]
        [Phone]
        public string Mobile { get; set; }
        public string Uid { get; set; }

        public ICollection<Orders> Orderss { get; set; }
    }
}
