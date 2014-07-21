using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareRoot
{
    class SquareRoot
    {
        static void Main(string[] args)
        {

            // 数値を入力してもらう
            // 入力された数値の平方根を求める
            // 小数点の入力に対応
            // 負の数の入力に対応

            // 目標精度 = 小数点以下11桁までとする
            int targetPrecision = 11;

            // 入力文字
            string s = null;
            double input = 0.0;

            bool finish_int = false;  // 整数部分
            bool finish = false;
            bool minus = false; 
            
            double previous = 0.0;   // 範囲の小さい方
            double next = 0.0;    // 範囲の大きい方
            double answer = 0.0;

            int i = 1;
            int j = 1;


            //Console.WriteLine(answer.ToString().Length);

            // 入力受信
            Console.Write("平方根を求める数を入力してください：");
            s = Console.ReadLine();

            try
            {
                input = double.Parse(s);
                if (input < 0)
                {
                    minus = true;
                    input *= -1;
                }

                // 整数の範囲を特定
                // 2 = 1.41421356237の場合、previous = 1, next = 2
                while (!finish_int)
                {
                    if (i * i == input)
                    {
                        answer = i;
                        finish_int = true;
                        finish = true;
                    }
                    else if (i * i > input)
                    {
                        finish_int = true;
                        previous = i - 1;
                        next = i;
                    }
                    i++;
                }

                // 平方根を求める　無理数を考慮して繰り返しは100回とする
                // 
                // 1回目
                // (1 + 2) / 2 = 1.5
                // 1.5 * 1.5 = 2.25
                // 2.25 > 2 [next update] 2 -> 1.5 *previous = 1, next = 1.5
                //
                // 2回目
                // (1+1.5) / 2 = 1.25
                // 1.25 * 1.25 = 1.5625
                // 1.5625 < 2 [previous update] 1 -> 1.25 *previous = 1.25, next = 1.5
                // 
                if (!finish) 
                {
                    while (j <= 100)
                    {
                        double temp = (previous + next) / 2;
                        double temp2 = temp * temp;
                        if (temp2 == input)
                        {
                            answer = temp;
                            finish = true;
                        }
                        else if (temp2 > input)
                        {
                            next = temp;
                            answer = previous;  // 超えてはいけない
                        }
                        else if (temp2 < input)
                        {
                            previous = temp;
                            answer = previous;  // 超えてはいけない
                        }
                        Console.WriteLine(answer.ToString());
                        j++;
                    }

                }

                answer = Math.Round(answer, targetPrecision);

            }
            catch(Exception e)
            {
                e.StackTrace.ToString();
            }

            if (minus)
            {
                answer *= -1;
            }

            Console.WriteLine("平方根は = " + answer.ToString());
            Console.WriteLine("stop");
        }
    }
}
