using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanoi
{
    class HanoiTower
    {
        static void Main(string[] args)
        {
            Hanoi(5, 'A', 'B', 'C');
            string temp = Console.ReadLine();
        }

        #region Hanoi
        /// <summary>
        /// recursive
        /// </summary>
        /// <param name="n"></param>
        /// <param name="from"></param>
        /// <param name="work"></param>
        /// <param name="dest"></param>
        public static void Hanoi(int n, char from, char work, char dest)
        {
            // criteria for break
            if (n >= 1)
            {
                Hanoi(n - 1, from, dest, work);
                Console.WriteLine("move:" + n.ToString() + " from:" + from.ToString() + " to:" + dest.ToString());
                Hanoi(n - 1, work, from, dest);
            }
        }
        #endregion
    }
}
