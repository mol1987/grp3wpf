using System;
using System.Collections.Generic;

namespace Library.TypeLib
{
    public class Order
    {
        public int? ID { get; set; }
        public DateTime? TimeCreated { get; set; }
        public int? Orderstatus { get; set; }
        public float? Price { get; set; }
        public int? CustomerID { get; set; }
        public List<Article>? Articles { get; set; }
    }
}
