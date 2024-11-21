using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR_Lib
{
    public class TextDecoration
    {
        public void UnderscoreMinus()
        {
            Console.WriteLine("-----------------------------------------------");
        }   
        public void UnderscoreColon()
        {
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::");
        }
        public void Col_White()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Col_Green()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
