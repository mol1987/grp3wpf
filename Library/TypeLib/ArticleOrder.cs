using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TypeLib
{
    public class ArticleOrder : ICloneable
    {
        public int ID { get; set; }
        public int OrdersID { get; set; }
        public int ArticlesID { get; set; }
        public int Count { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}
