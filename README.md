# TSVFile - 資料檔讀取與自訂類別集合專案 1133334楊恩奇

[cite_start]本專案為「視窗程式設計 (II)」課程之專案，核心目標是學習如何讀取、解析帶有定位點分隔（Tab-Separated Values, TSV）的文字資料檔 [cite: 1][cite_start]，並結合 C# 物件導向技術與 Windows Forms 控制項，將資料結構化呈現於介面上 [cite: 8, 13]。

## 📌 專案核心目標 (前 29 頁核心)
1. [cite_start]**理解 TSV 檔案讀取機制**：學習如何處理非逗號分隔（如 Tab 鍵分隔）的表格資料 [cite: 15]。
2. [cite_start]**解決文字編碼問題**：掌握將 Excel 檔案另存為 `Unicode 文字 (*.txt)` 的方法，以確保特殊符號與音標能夠正常顯示 [cite: 4, 5]。
3. [cite_start]**物件導向實作**：動態建立自訂類別（`WordItem`）[cite: 13] [cite_start]與自訂類別集合（`WordCollection`）來封裝單字資料 。

---

## 🛠️ 開發環境與控制項配置

### 1. 介面控制項配置 (`frmTSVFile`)
[cite_start]主表單包含以下核心 UI 控制項與屬性設定 [cite: 8]：
* [cite_start]**MenuStrip (`mnsword`)**：表單置頂（`Dock: Top`）[cite: 8][cite_start]，提供功能表選項 [cite: 10]：
    * [cite_start]`File` ➡️ `Open` (`tsmiOpen`)、`Exit` (`tsmiExit`) [cite: 10]
    * [cite_start]`Help` ➡️ `About` (`tsmiAbout`) [cite: 10]
* [cite_start]**ListView (`lvwWord`)**：填滿剩餘視窗（`Dock: Fill`）[cite: 8][cite_start]，用於呈現結構化單字清單 [cite: 22]。
    * [cite_start]`View`: `Details` (詳細資料模式) [cite: 8]
    * [cite_start]`FullRowSelect`: `True` (整列選取) [cite: 8]
    * [cite_start]**欄位(ColumnHeader)配置**：包含「單字」、「音標」、「音檔路徑」、「解釋」四個資料欄，預設寬度皆為 `60` [cite: 9]。
* [cite_start]**StatusStrip (`ssrword`)**：位於表單底部（`Dock: Bottom`）[cite: 8]。
    * [cite_start]內含 **ToolStripStatusLabel (`tsslMessage`)**：用於即時顯示系統載入狀態訊息 [cite: 8]。

### 2. 關於表單整合 (`frmAbout`)
* [cite_start]專案內建整合了標準 `About` 表單 [cite: 10, 11][cite_start]，透過修改專案屬性中的「組件資訊」自動同步產品名稱、版本、著作權等欄位 [cite: 11]，無需手動在畫面刻字。

---

## 💻 核心程式碼架構

### 一、 基礎單字類別：`WordItem.cs`
[cite_start]負責封裝單一筆單字的完整資訊，並提供解析 `\t` 分隔字串的建構子 [cite: 13, 15]。

```csharp
public class WordItem
{
    public string Word { get; set; }
    public string Phonogram { get; set; }
    public string SoundPath { get; set; }
    public string Explain { get; set; }

    /// <summary>
    /// 建構子：傳入單行 Tab 分隔字串並自動解析
    /// </summary>
    public WordItem(string str)
    {
        string[] strLists = str.Split('\t'); // 用 Tab 分隔字串
        if (strLists.Length >= 3)
        {
            Word = strLists[0];
            Phonogram = strLists[1];
            SoundPath = strLists[2];
            // 若解釋欄位內含有多個 Tab 或換行，將其餘欄位重新結合
            Explain = string.Join(Environment.NewLine, strLists.Skip(3));
        }
    }

    public override string ToString()
    {
        return Word;
    }
}
