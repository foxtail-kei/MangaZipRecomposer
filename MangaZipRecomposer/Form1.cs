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

namespace MangaZipRecomposer
{
    public partial class Form1 : Form
    {
        public static string EXTENSION_ZIP = ".ZIP";
        public static string SRC_FILE_PREFIX = "cbr_";

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
            string[] pathList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach(string path in pathList) {
                String ext = Path.GetExtension(path);
                if (!ext.ToUpper().Equals(EXTENSION_ZIP)) {
                    continue;
                }
                String fileName = Path.GetFileName(path);
                if (!fileName.StartsWith(SRC_FILE_PREFIX)) {
                    continue;
                }
                targetFileList.Items.Add(path);
            }
        }
    }
}
