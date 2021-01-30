using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace MangaZipRecomposer
{
    public partial class Form1 : Form
    {
        public static string EXTENSION_ZIP = ".ZIP";
        public static string SRC_FILE_PREFIX = "cbr_";
        public static string TEMP_FOLDER = "MangaZipRecomposer";
        public static string UNZIP_FOLDER = "\\unzip";
        public static string PICKUP_FOLDER = "\\pickup";

        public Form1()
        {
            InitializeComponent();
            this.targetFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.targetFileList_DragDrop);
            this.targetFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.targetFileList_DragEnter);
        }

        private void targetFileList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void targetFileList_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void targetFileList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            String[] pathList = (String[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach(String path in pathList) {
                String ext = Path.GetExtension(path);
                if (!ext.ToUpper().Equals(EXTENSION_ZIP)) {
                    continue;
                }
                String fileName = Path.GetFileName(path);
                if (!fileName.StartsWith(SRC_FILE_PREFIX)) {
                    continue;
                }
                if (targetFileList.Items.Contains(path)) continue;

                targetFileList.Items.Add(path);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int index = targetFileList.SelectedIndex;
            if (index > 0) targetFileList.Items.RemoveAt(index);
        }

        private void deleteAllButton_Click(object sender, EventArgs e)
        {
            targetFileList.Items.Clear();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            String tempPath = Path.GetTempPath() + TEMP_FOLDER;
            
            foreach (String path in targetFileList.Items) { 
                // ファイルの存在チェック
                if (!File.Exists(path)) {
                    continue;
                }

                try
                {
                    // ファイルの解凍
                    ZipFile.ExtractToDirectory(path, tempPath + UNZIP_FOLDER);
                    String unzipPath = Directory.GetDirectories(tempPath + UNZIP_FOLDER)[0];

                    // 作業フォルダ作成
                    String pickupPath = tempPath + PICKUP_FOLDER;
                    Directory.CreateDirectory(pickupPath);

                    // 対象ファイルを移動
                    // - 表紙を移動


                    // - その他のページを移動

                    // ファイルの圧縮

                } catch {
                } finally {
                    if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true);
                }
            }
        }
    }
}
