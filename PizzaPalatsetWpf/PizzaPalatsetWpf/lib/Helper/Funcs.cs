using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Helper
{
    public class Funcs
    {
        public dynamic ParseValue(string val)
        {
            string stringval;
            int intval;
            float floatval;
            if (float.TryParse(val, out floatval))
            {
                return floatval;
            };
            if (Int32.TryParse(val, out intval))
            {
                return intval;
            };
            return val;
        }
    }
}
