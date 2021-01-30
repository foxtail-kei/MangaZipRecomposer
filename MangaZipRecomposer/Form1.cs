﻿using System;
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
        public static string IMAGES_FOLDER = "\\images";
        public static string COVER_SRC_FILE = "\\cover.jpeg";
        public static string COVER_DEST_FILE = "\\00001.jpeg";

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
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void targetFileList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            String[] pathList = (String[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach(String path in pathList) {
                String ext = Path.GetExtension(path);
                if (!ext.ToUpper().Equals(EXTENSION_ZIP)) 
                {
                    continue;
                }
                String fileName = Path.GetFileName(path);
                if (!fileName.StartsWith(SRC_FILE_PREFIX)) 
                {
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

        private async void startButton_Click(object sender, EventArgs e)
        {
            // コントロールをロック
            targetFileList.Enabled = false;
            deleteButton.Enabled = false;
            deleteAllButton.Enabled = false;

            // プログレスバーを初期化
            progressBar.Minimum = 0;
            progressBar.Maximum = targetFileList.Items.Count * 15;
            progressBar.Value = 0;

            // 別スレッドで処理を開始
            await Task.Run(() =>
            {
                recomposeThread();
            });

            // リストをクリア
            targetFileList.Items.Clear();

            // コントロールのロックを解除
            targetFileList.Enabled = true;
            deleteButton.Enabled = true;
            deleteAllButton.Enabled = true;
        }

        private void recomposeThread()
        {
            // 作業フォルダの初期化
            String tempPath = Path.GetTempPath() + TEMP_FOLDER;
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }

            try
            {
                // リストのアイテムを並列処理
                Parallel.ForEach(targetFileList.Items.Cast<String>(), path =>
                {
                    try
                    {
                        recompose(path);
                    }
                    finally
                    {
                        reportProgress();
                    }
                });
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }

        private void recompose(String path)
        {
            // ファイルの存在チェック
            if (!File.Exists(path))
            {
                reportProgress();
                reportProgress();
                return;
            }

            // 作業フォルダのパスを作成
            String tempPath = Path.GetTempPath() + TEMP_FOLDER + "\\" + Path.GetFileNameWithoutExtension(path);

            try
            {
                // ファイルの解凍
                ZipFile.ExtractToDirectory(path, tempPath);
                String unzipPath = Directory.GetDirectories(tempPath)[0];

                reportProgress();

                // 表紙を移動
                File.Move(unzipPath + COVER_SRC_FILE, unzipPath + IMAGES_FOLDER + COVER_DEST_FILE);

                // ファイルの圧縮
                String destFilePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileName(path).Substring(SRC_FILE_PREFIX.Length);
                ZipFile.CreateFromDirectory(unzipPath + IMAGES_FOLDER, destFilePath);

                reportProgress();

                // 元ファイル削除
                if (deleteSrcFileCheck.Checked)
                {
                    File.Delete(path);
                }
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }

        private void reportProgress()
        {
            progressBar.Invoke(new Action(() =>
            {
                progressBar.PerformStep();
            }));
        }
    }
}
