using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace skillup
{
    class FizzBuzz
    {
        static void Main(string[] args)
        {
            // start
            Console.WriteLine("Start ------------- ");

            // declare
            int i = 1;
            string fizzBuzz = "FizzBuzz";
            string fizz     = "Fizz";
            string buzz     = "Buzz";

            // FizzBuzz 
            while (i <= 100)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine(fizzBuzz);
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine(fizz);
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine(buzz);
                }
                else
                {
                    Console.WriteLine(i.ToString());
                }
                
                i++;
            }

            Console.WriteLine("End ------------- ");
        }
    }
}
