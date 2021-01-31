using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

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

        public static string START_BUTTON = "処理開始";
        public static string STOP_BUTTON = "中止";

        private SwitchMode startSwitchMode = SwitchMode.Start;
        private CancellationTokenSource cancellationTokenSource;

        private enum SwitchMode
        {
            Start,
            Stop
        }

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
            switch (startSwitchMode)
            {
                case SwitchMode.Start:
                    // 処理開始
                    // スタートスイッチをキャンセルに変更
                    startButton.Text = STOP_BUTTON;
                    startSwitchMode = SwitchMode.Stop;

                    // コントロールをロック
                    lockControll();

                    // プログレスバーを初期化
                    progressBar.Minimum = 0;
                    progressBar.Maximum = targetFileList.Items.Count * 15;
                    progressBar.Value = 0;
                    SetState(progressBar, ProgressBarStateEnum.Normal);
                    progressBar.Update();

                    // キャンセルトークンの準備
                    cancellationTokenSource = new CancellationTokenSource();
                    CancellationToken cToken = cancellationTokenSource.Token;

                    // 別スレッドで処理を開始
                    bool result = await Task.Run(() => recomposeThread(cToken));

                    // リストをクリア
                    if (result)
                    {
                        targetFileList.Items.Clear();
                    }

                    // スタートボタンを元に戻す
                    startButton.Text = START_BUTTON;
                    startSwitchMode = SwitchMode.Start;
                    startButton.Enabled = true;

                    // コントロールのロックを解除
                    unlockControll();

                    break;
                case SwitchMode.Stop:
                    // スタートボタンをロックする
                    startButton.Enabled = false;

                    // 中止
                    cancellationTokenSource.Cancel();

                    // プログレスバーを中止状態にする
                    SetState(progressBar, ProgressBarStateEnum.Error);

                    break;
            }
        }

        private bool recomposeThread(CancellationToken cToken)
        {
            // 作業フォルダの初期化
            String tempPath = Path.GetTempPath() + TEMP_FOLDER;
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }

            // 処理中断の準備
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cToken;

            try
            {
                // リストのアイテムを並列処理
                Parallel.ForEach(targetFileList.Items.Cast<String>(), parallelOptions, path =>
                {
                    parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                    recompose(path, parallelOptions);
                    reportProgress();
                });
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }

            return true;
        }

        private void recompose(String path, ParallelOptions parallelOptions)
        {
            // ファイルの存在チェック
            if (!File.Exists(path))
            {
                reportProgress(2);
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
                parallelOptions.CancellationToken.ThrowIfCancellationRequested();

                // 表紙を移動
                File.Move(unzipPath + COVER_SRC_FILE, unzipPath + IMAGES_FOLDER + COVER_DEST_FILE);

                // ファイルの圧縮
                String destFilePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileName(path).Substring(SRC_FILE_PREFIX.Length);
                ZipFile.CreateFromDirectory(unzipPath + IMAGES_FOLDER, destFilePath);

                reportProgress();
                parallelOptions.CancellationToken.ThrowIfCancellationRequested();

                // 元ファイル削除
                if (deleteSrcFileCheck.Checked)
                {
                    File.Delete(path);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }

        private void reportProgress(int times = 1)
        {
            progressBar.Invoke(new Action(() =>
            {
                for (int i = 0; i < times; i++)
                {
                    progressBar.PerformStep();
                }
            }));
        }

        private void lockControll()
        {
            // コントロールをロック
            targetFileList.Enabled = false;
            deleteButton.Enabled = false;
            deleteAllButton.Enabled = false;
            deleteSrcFileCheck.Enabled = false;
        }

        private void unlockControll()
        {
            // コントロールのロックを解除
            targetFileList.Enabled = true;
            deleteButton.Enabled = true;
            deleteAllButton.Enabled = true;
            deleteSrcFileCheck.Enabled = true;
        }

        const int WM_USER = 0x400;
        const int PBM_SETSTATE = WM_USER + 16;
        const int PBM_GETSTATE = WM_USER + 17;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public enum ProgressBarStateEnum : int
        {
            Normal = 1,
            Error = 2,
            Paused = 3,
        }

        private void SetState(ProgressBar pBar, ProgressBarStateEnum state)
        {
            SendMessage(pBar.Handle, PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ウィンドウの位置・サイズを復元
            Bounds = Properties.Settings.Default.Bounds;

            // オプションの設定値を復元
            deleteSrcFileCheck.Checked = Properties.Settings.Default.DeleteSrcFile;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ウィンドウの位置・サイズを保存
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Bounds = Bounds;
            }
            else
            {
                Properties.Settings.Default.Bounds = RestoreBounds;
            }

            // オプションの設定値を保存
            Properties.Settings.Default.DeleteSrcFile = deleteSrcFileCheck.Checked;

            Properties.Settings.Default.Save();
        }
    }
}
