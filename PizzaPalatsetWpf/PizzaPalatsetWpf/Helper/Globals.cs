using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;

namespace PizzaPalatsetWpf
{
    public class Globals
    {
       public enum ArticleTypes
        {
            Pizza, Pasta, Sallad, Dricka, Tillbehor, Rekommenderat, Alla
        }
        public NotifyTaskCompletion<IEnumerable<Articles>> Articles { get; set; }


    }
}
