using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosuJudge
{
    class Program
    {
        static void Main(string[] args)
        {

            //①与えられた数が素数かどうか調べる
            //②与えられた数までの素数を列挙する
            //③処理にかかった時間を計測する

            DateTime startTime = DateTime.Now.ToLocalTime();
            Console.WriteLine("start: " + startTime.ToString());
                        
            // 引数をとる
            Console.WriteLine(args[0].ToString());
            int limit = int.Parse(args[0].ToString());
            PracUtil pracutil = new PracUtil();

            for (int i = 1; i <= limit; i++)
            {
                if (pracutil.isSosu(i))
                {
                    Console.Write(i);
                    Console.Write(", ");
                }
            }

            // finish
            DateTime finishTime = DateTime.Now.ToLocalTime(); 
            Console.WriteLine("");
            Console.WriteLine("finish: " + finishTime.ToString());

            // stop
            Console.WriteLine("stop");
        }
    }

    public class PracUtil
    {
        /// <summary>
        /// 引数が素数かどうか判定する
        /// </summary>
        /// <param name="limit"></param>
        /// <returns>true = 素数, false = 素数ではない</returns>
        public Boolean isSosu(int limit)
        {
            int count = 0;
            int i = 1;
            Boolean judge = false;

            // 引数の値まで繰り返し除算を試みる
            for (i = 1; i <= limit; i++)
            {

                int answer = limit % i;
                if (answer == 0)
                {
                    count++;
                }

                if (count == 2)
                {
                    break;
                }
            }

            // 素数判定
            if (i == limit || limit == 1)
            {
                judge = true;
            }

            return judge;

        }
    }
}

