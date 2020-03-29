using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Helper
{
    public class Color
    {
        public class ForeGround
        {
            public static string Black = "\u001b[30m";
            public static string Red = "\u001b[31m";
            public static string Green = "\u001b[32m";
            public static string Yellow = "\u001b[33m";
            public static string Blue = "\u001b[34m";
            public static string Magenta = "\u001b[35m";
            public static string Cyan = "\u001b[36m";
            public static string White = "\u001b[37m";
        }
        public class BackGround
        {
            public static string Black = "\u001b[40m";
            public static string Red = "\u001b[41m";
            public static string Green = "\u001b[42m";
            public static string Yellow = "\u001b[43m";
            public static string Blue = "\u001b[44m";
            public static string Magenta = "\u001b[45m";
            public static string Cyan = "\u001b[46m";
            public static string White = "\u001b[47m";
        }
        public static string Bright = ";1m";
        public static string Reset = "\u001b[0m";
    }
}
