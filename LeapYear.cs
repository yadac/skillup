using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LeapYear
{
    class LeapYear
    {
        static void Main(string[] args)
        {
            // 入力された整数がグレゴリオ暦（いつも使ってるやつ）でうるう年であるか判定せよ
            //
            // グレゴリオ暦では、次の規則に従って400年間に（100回ではなく）97回の閏年を設ける。 from wikipedia
            // (1)西暦年が4で割り切れる年は閏年
            // (2)ただし、西暦年が100で割り切れる年は平年
            // (3)ただし、西暦年が400で割り切れる年は閏年

            // declare
            string receive = "";
            int intReceive = 0;
            bool leapYear = false;

            // 入力文字列受け取り
            Console.Write("閏年判定をする西暦を数値で入力してください(ex.2000)：");
            receive = Console.ReadLine();

            //// 数値判定
            //if (!Regex.IsMatch(receive, @"[0-9]+$"))
            //{
            //    Console.WriteLine("数値を入力してください");
            //}

            try
            {
                intReceive = int.Parse(receive);
            }
            catch (Exception e)
            {
                Console.WriteLine("西暦は数値で入力してください");
            }

            // (1)の条件判定
            if (intReceive % 4 == 0)
            {
                leapYear = true;
            }

            // (2)の条件判定
            if (intReceive >= 100 && intReceive % 100 == 0)
            {
                leapYear = false;
            }

            // (3)の条件判定
            if (intReceive >= 400 && intReceive % 400 == 0)
            {
                leapYear = true;
            }

            if (leapYear)
            {
                Console.WriteLine(intReceive.ToString() + " は閏年です");
            }
            else
            {
                Console.WriteLine(intReceive.ToString() + " は閏年ではありません");
            }

            // Console.Write("Enterで終了");
            Console.ReadLine();
        }
    }
}
