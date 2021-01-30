namespace MangaZipRecomposer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.targetFileList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.deleteSrcFileCheck = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // targetFileList
            // 
            this.targetFileList.AllowDrop = true;
            this.targetFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetFileList.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.targetFileList.FormattingEnabled = true;
            this.targetFileList.IntegralHeight = false;
            this.targetFileList.ItemHeight = 15;
            this.targetFileList.Location = new System.Drawing.Point(14, 27);
            this.targetFileList.Name = "targetFileList";
            this.targetFileList.Size = new System.Drawing.Size(614, 405);
            this.targetFileList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "処理対象ファイル";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.deleteButton.Location = new System.Drawing.Point(650, 27);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(96, 34);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "削除";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAllButton.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.deleteAllButton.Location = new System.Drawing.Point(650, 78);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(96, 34);
            this.deleteAllButton.TabIndex = 2;
            this.deleteAllButton.Text = "全て削除";
            this.deleteAllButton.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.startButton.Location = new System.Drawing.Point(650, 383);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(96, 49);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "処理開始";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // deleteSrcFileCheck
            // 
            this.deleteSrcFileCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSrcFileCheck.AutoSize = true;
            this.deleteSrcFileCheck.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.deleteSrcFileCheck.Location = new System.Drawing.Point(650, 163);
            this.deleteSrcFileCheck.Name = "deleteSrcFileCheck";
            this.deleteSrcFileCheck.Size = new System.Drawing.Size(96, 19);
            this.deleteSrcFileCheck.TabIndex = 3;
            this.deleteSrcFileCheck.Text = "元ファイル削除";
            this.deleteSrcFileCheck.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(647, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "オプション";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 446);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.deleteSrcFileCheck);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.deleteAllButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetFileList);
            this.MinimumSize = new System.Drawing.Size(774, 485);
            this.Name = "Form1";
            this.Text = "MangaZipRecomposer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox targetFileList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button deleteAllButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox deleteSrcFileCheck;
        private System.Windows.Forms.Label label2;
    }
}

