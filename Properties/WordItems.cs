using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TSVfile.Properties
{
    class WordItems
    {
    }
    public class WordItem
    {
        public string Word { get; set; }
        public string Phonogram { get; set; }
        public string SoundPath { get; set; }
        public string Explain { get; set; }

        public WordItem(string str)
        {
            string[] strLists = str.Split('\t');

            if (strLists.Length >= 3)
            {
                Word = strLists[0];
                Phonogram = strLists[1];
                SoundPath = strLists[2];
                Explain = string.Join(Environment.NewLine,
                                      strLists.Skip(3));
            }
        }

        public override string ToString()
        {
            return Word;
        }
        public class WordCollection : Collection<WordItem>
        {
            /// <summary>
            /// 從字串陣列載入資料
            /// </summary>
            /// <param name="lines">輸入的單字字串陣列</param>
            public void LoadFromStringArray(string[] lines)
            {
                this.Clear(); // 清空現有的資料
                              // 將字串陣列的資料載入到 WordCollection 物件中
                foreach (string line in lines)
                {
                    // 產生 WordItem 物件
                    WordItem item = new WordItem(line);
                    this.Add(item);
                }
            }
        }
        /// <summary>
        /// 單字清單
        /// </summary>
        WordItems _WordList = new WordItems();
        /// <summary>
        /// 關於視窗
        /// </summary>
        frmAbout about = new frmAbout();
    }
}
