using System;

namespace Library.TypeLib
{
    public class Ingredient
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }

        override public string ToString()
        {
            return Name;
        }
    }
}
