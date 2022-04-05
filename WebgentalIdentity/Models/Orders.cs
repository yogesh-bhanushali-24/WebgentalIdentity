using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebgentalIdentity.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("applicationUser")]
        public string UserId { get; set; }
        [ForeignKey("addressModels")]
        public int AddressId { get; set; }
        [ForeignKey("Product")]
        public int Pid { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public virtual Product Product { get; set; }
        public virtual AddressModel addressModels { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }


    }
}
