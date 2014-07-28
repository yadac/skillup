using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;

namespace WordChain
{
    class WordChain
    {
        static void Main(string[] args)
        {
            // --- 仕様 ---
            //あらかじめ用意した「辞書ファイル」の内容に基づいて、ユーザーとしりとりをする対話型プログラム。
            //辞書ファイルのフォーマットは、キャリッジリターンで区切られた半角英字のみのテキストファイルとする。
            //このプログラムのおおまかな仕様を以下に示す。
            //乱数により、先攻と後攻を決定する。ユーザーが先攻の場合はランダムで最初の文字を決定し、コンピュータが先攻の場合はランダムで最初の単語を出力する
            //ユーザーが辞書にない単語を入力すると再入力を促す（半角英字以外の文字が入っていてもメッセージは同じでよい）
            //すでに使われた単語をユーザーが入力すると、以下のメッセージを出力して終了する。「その言葉は [回数] 回目に [あなた|わたし] が使用しています。わたしの勝ちです。今回のしりとりでは [使用単語数] 個の単語を使用しました。」
            //コンピュータの番で、辞書の中に使える単語が残ってない場合、以下のメッセージを出力して終了する。「まいりました！あなたの勝ちです。　今回のしりとりでは [使用単語数] 個の単語を使用しました。」

            // --- todo ---
            // ユーザ側が使えるワードがなくなったらどーするか？

            // 宣言
            string receiveWord = "";
            string nextWord = "";
            string lastCharacter = "";

            Computer computer = new Computer();

            // 辞書の読込
            computer.readDictionary();

            // 先攻を決める乱数生成
            Random rnd = new Random();
            int randomNumber = rnd.Next(100);   // 100に意味はない

            // 偶数ならユーザが先攻、奇数ならコンピュータが先攻
            if (randomNumber % 2 == 0)
            {
                // ランダムで最初の文字を決定する
                string abc = "abcdefghijklmnopqrstuvwxyz";
                Random rnd2 = new Random();
                int index = rnd2.Next(26);  // 0-26　アルファベットの総数
                lastCharacter = abc.Substring(index, 1);
                computer.setLastCharacter(lastCharacter);
                Console.WriteLine(lastCharacter + " から始めましょう");
            }
            else
            {
                // ランダムで最初の単語を出力する
                nextWord = computer.getNextWord(receiveWord);
                Console.WriteLine(nextWord + " から始めましょう");
            }

            // ユーザとの対話（loop）
            while (true)
            {
                Console.WriteLine("あなたの番です。英単語を入力してください。");
                receiveWord = Console.ReadLine();

                // 入力値チェック
                if (!computer.checkWord(receiveWord))
                {
                    Console.WriteLine("入力値不正");
                    continue;
                }
                // コンピュータ処理
                nextWord = computer.getNextWord(receiveWord);
                if (computer.getComStatus() == (int)Status.LOOSE || computer.getComStatus() == (int)Status.WIN)
                {
                    Console.WriteLine(nextWord);
                    break;
                }
                // 臨場感
                Thread.Sleep(500);
                Console.WriteLine("Computer:" + nextWord);
            }
            Console.WriteLine("--------------------------------------------> finish");
        }
    }

    // 列挙型
    public enum Status
    {
        NORMAL = 0,
        WIN = 1,
        LOOSE = 2
    }


    class Computer
    {

        List<string> word = new List<string>();      // 全リスト
        List<string> notUsed = new List<string>();      // 未使用
        List<string> Used = new List<string>();         // 使用済み
        private string lastCharacter = "";
        private int comStatus;
        private bool top = false;
        //private string resultMessage = "";

        public Computer()
        {
            comStatus = (int)Status.NORMAL;
        }

        /// <summary>
        /// 辞書ファイル（テキスト）をロードする。
        /// </summary>
        public void readDictionary()
        {
            // テキストファイル読込
            StreamReader sr = new StreamReader("C:\\Users\\yosuke\\Desktop\\2014_skillup\\eword.txt", Encoding.GetEncoding("Shift_JIS"));
            while (sr.Peek() >= 0)
            {
                string text = sr.ReadLine();
                notUsed.Add(text);
                word.Add(text);
            }
            sr.Close();
        }

        /// <summary>
        /// 次のワードを返却する。
        /// </summary>
        /// <param name="re">相手の入力ワード。空文字の場合 == コンピュータ先攻の一手目。</param>
        /// <returns>次のワード</returns>
        public string getNextWord(string re)
        {
            // 次のワード
            string nextWord = "";

            if (re == "")
            {
                // 空文字の場合は未使用リストからランダムでワードを抽出
                Random rnd3 = new Random();
                int index = rnd3.Next(notUsed.Count);
                nextWord = notUsed[index].ToString();
                top = true; // コンピュータ先攻
            }
            else
            {
                // 使用済みワードが入力された場合 == コンピュータの勝ち
                if (Used.Contains(re))
                {
                    int number = Used.IndexOf(re);
                    string who = "あなた";
                    if (top)
                    {
                        if (number % 2 == 0)
                        {
                            who = "わたし";
                        }
                    }
                    else
                    {
                        if (number % 2 != 0)
                        {
                            who = "わたし";
                        }
                    }
                    setComStatus((int)Status.WIN);
                    nextWord = "その言葉は [" + (number + 1).ToString() + "] 回目に [" + who + "] が使用しています。わたしの勝ちです。今回のしりとりでは [" + Used.Count().ToString() + "] 個の単語を使用しました。";
                    return nextWord;
                }

                // 未使用リストから除外
                notUsed.Remove(re);

                // 使用済みリストに移動
                Used.Add(re);

                // 未使用リストをシャッフル（Fisher-Yatesアルゴリズム）
                Random rng = new System.Random();
                int n = notUsed.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    string tmp = notUsed[k].ToString();
                    notUsed[k] = notUsed[n];
                    notUsed[n] = tmp;
                }

                // 未使用リストから先頭文字が一致するワードを取得
                lastCharacter = re.Substring(re.Length - 1);
                IEnumerator ie = notUsed.GetEnumerator();   // alternative foreach. just training.
                while (ie.MoveNext())
                {

                    string tempWord = ie.Current.ToString();
                    if (tempWord.StartsWith(lastCharacter))
                    {
                        nextWord = tempWord;
                        break;
                    }
                }
                // 次のワードが見つからなかった場合 == コンピュータの負け
                if (nextWord == "")
                {
                    setComStatus((int)Status.LOOSE);
                    nextWord = "まいりました！あなたの勝ちです。今回のしりとりでは [" + Used.Count().ToString() + "] 個の単語を使用しました。";
                    return nextWord;
                }
            }

            // 最後の一文字（相手の入力値の判定に必要）
            lastCharacter = nextWord.Substring(nextWord.Length - 1);

            // 未使用リストから除外
            notUsed.Remove(nextWord);

            // 使用済みリストに移動
            Used.Add(nextWord);

            return nextWord;
        }

        /// <summary>
        /// getter
        /// </summary>
        /// <returns></returns>
        public string getLastCharacter()
        {
            return this.lastCharacter;
        }

        /// <summary>
        /// setter
        /// </summary>
        /// <param name="s"></param>
        public void setLastCharacter(string s)
        {
            this.lastCharacter = s;
        }

        /// <summary>
        /// getter
        /// </summary>
        /// <returns></returns>
        public int getComStatus()
        {
            return this.comStatus;
        }

        /// <summary>
        /// setter
        /// </summary>
        /// <param name="i"></param>
        public void setComStatus(int i)
        {
            this.comStatus = i;
        }

        /// <summary>
        /// 入力値の検証。
        /// ①空文字でないか
        /// ②入力フォーマットが正しいか（半角英字）
        /// ③先頭の文字が正しいか
        /// ④リストに存在するか
        /// </summary>
        /// <param name="re"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        public Boolean checkWord(string re)
        {
            // 空文字でないか
            if (re == "")
            {
                return false;
            }

            // 入力フォーマットが正しいか（半角英字）
            if (!Regex.IsMatch(re.ToLower(), @"[a-z]+$"))
            {
                return false;
            }
            // 先頭の文字が正しいか
            if (!re.StartsWith(getLastCharacter()))
            {
                return false;
            }
            // リストに存在するか
            if (!word.Contains(re))
            {
                return false;
            }

            return true;
        }

    }
}
