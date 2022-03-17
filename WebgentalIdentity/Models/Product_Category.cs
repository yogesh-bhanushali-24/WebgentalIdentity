using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebgentalIdentity.Models;

namespace WebgentalIdentity.Models
{
    public class Product_Category
    {
        public Product prod { get; set; }
        public Category Cate { get; set; }
    }
}
