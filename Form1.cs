using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSVfile.Properties;
using static TSVfile.Properties.WordItem;
using System.IO;

namespace TSVfile
{
    public partial class frmTSVFile: Form
    {
        /// <summary>
        /// 單字清單
        /// </summary>
        WordCollection _WordList = new WordCollection();

        frmAbout about = new frmAbout();
        public frmTSVFile()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tsslMessage.Text = "請開啟檔案";
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            about.ShowDialog(this);
        }
        private void UpdateListView()
        {
            lvwWord.BeginUpdate();
            // 清除 ListView 的所有項目
            lvwWord.Items.Clear();
            //暫停重繪
            // 將 WordCollection 物件中的資料載入到 ListView 中
            foreach (WordItem item in _WordList)
            {
                // 建立 ListViewItem 物件
                ListViewItem lvi = new ListViewItem(item.Word);
                lvi.SubItems.Add(item.Phonogram);
                lvi.SubItems.Add(item.SoundPath);
                lvi.SubItems.Add(item.Explain);
                // 將 ListViewItem 物件加入到 ListView 中
                lvwWord.Items.Add(lvi);
            }
            lvwWord.EndUpdate();  //重繪;
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TSV files (*.tsv)|*.tsv|Text files (*.txt)|*.txt|All files (*.*) | *.* ";
            ofd.Title = "開啟檔案";
            // 設定初始目錄為程式所在目錄
            ofd.InitialDirectory = Application.StartupPath;
            DialogResult dr = ofd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                // 讀取檔案並且將每一行的資料放入字串陣列
                string[] lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                // 將字串陣列的資料載入到 WordCollection 物件中
                _WordList.LoadFromStringArray(lines);
                // 將 WordCollection 物件中的資料載入到 ListView 中
                UpdateListView();
                this.tsslMessage.Text = $"{_WordList.Count} 單字已成功載入";
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTSVFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("確定要離開嗎?", "離開",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true; // 取消關閉
            }
        }
    }
}
