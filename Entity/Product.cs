using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAdmin.Entity
{
    public class Product
    {
        
            public string Sku { get; set; }
            public string Name { get; set; }
            public string desc { get; set; }
            public decimal Price { get; set; }
            public string Url { get; set; }
            public int Stock { get; set; }
        }
    }

