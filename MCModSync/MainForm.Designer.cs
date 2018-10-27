namespace MCModSync {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.RepositoryUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ModDirectoryPath = new System.Windows.Forms.TextBox();
            this.MinecraftProfileList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LocalModList = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CheckModUpdate = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.Dummy = new System.Windows.Forms.Label();
            this.ModListProgressBar = new System.Windows.Forms.ProgressBar();
            this.FileSystemWatcher = new System.IO.FileSystemWatcher();
            this.ModCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RepositoryUrl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 26);
            this.panel1.TabIndex = 4;
            // 
            // RepositoryUrl
            // 
            this.RepositoryUrl.BackColor = System.Drawing.SystemColors.Window;
            this.RepositoryUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RepositoryUrl.Cursor = System.Windows.Forms.Cursors.Default;
            this.RepositoryUrl.Location = new System.Drawing.Point(68, 5);
            this.RepositoryUrl.Name = "RepositoryUrl";
            this.RepositoryUrl.ReadOnly = true;
            this.RepositoryUrl.Size = new System.Drawing.Size(339, 16);
            this.RepositoryUrl.TabIndex = 1;
            this.RepositoryUrl.TabStop = false;
            this.RepositoryUrl.Text = "RepositoryUrl";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "リポジトリ:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ModDirectoryPath);
            this.panel2.Controls.Add(this.MinecraftProfileList);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(410, 47);
            this.panel2.TabIndex = 5;
            // 
            // ModDirectoryPath
            // 
            this.ModDirectoryPath.BackColor = System.Drawing.SystemColors.Window;
            this.ModDirectoryPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModDirectoryPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.ModDirectoryPath.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ModDirectoryPath.Location = new System.Drawing.Point(80, 28);
            this.ModDirectoryPath.Name = "ModDirectoryPath";
            this.ModDirectoryPath.ReadOnly = true;
            this.ModDirectoryPath.Size = new System.Drawing.Size(327, 14);
            this.ModDirectoryPath.TabIndex = 2;
            this.ModDirectoryPath.TabStop = false;
            this.ModDirectoryPath.Text = "ModDirectoryPath";
            // 
            // MinecraftProfileList
            // 
            this.MinecraftProfileList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MinecraftProfileList.FormattingEnabled = true;
            this.ErrorProvider.SetIconPadding(this.MinecraftProfileList, 5);
            this.MinecraftProfileList.Location = new System.Drawing.Point(80, 3);
            this.MinecraftProfileList.Name = "MinecraftProfileList";
            this.MinecraftProfileList.Size = new System.Drawing.Size(305, 23);
            this.MinecraftProfileList.TabIndex = 1;
            this.MinecraftProfileList.TabStop = false;
            this.MinecraftProfileList.SelectedIndexChanged += new System.EventHandler(this.MinecraftProfileList_SelectedIndexChanged);
            this.MinecraftProfileList.MouseEnter += new System.EventHandler(this.FocusToControl);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "プロファイル:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ModCount);
            this.panel3.Controls.Add(this.ModListProgressBar);
            this.panel3.Controls.Add(this.LocalModList);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(12, 102);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 211);
            this.panel3.TabIndex = 6;
            // 
            // LocalModList
            // 
            this.LocalModList.CheckOnClick = true;
            this.LocalModList.FormattingEnabled = true;
            this.LocalModList.Location = new System.Drawing.Point(8, 23);
            this.LocalModList.Name = "LocalModList";
            this.LocalModList.Size = new System.Drawing.Size(399, 184);
            this.LocalModList.TabIndex = 1;
            this.LocalModList.TabStop = false;
            this.LocalModList.MouseEnter += new System.EventHandler(this.FocusToControl);
            this.LocalModList.MouseLeave += new System.EventHandler(this.FocusToDummy);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "更新対象外Mod設定:";
            // 
            // CheckModUpdate
            // 
            this.CheckModUpdate.Location = new System.Drawing.Point(286, 321);
            this.CheckModUpdate.Name = "CheckModUpdate";
            this.CheckModUpdate.Size = new System.Drawing.Size(136, 28);
            this.CheckModUpdate.TabIndex = 7;
            this.CheckModUpdate.TabStop = false;
            this.CheckModUpdate.Text = "Mod の更新を確認";
            this.CheckModUpdate.UseVisualStyleBackColor = true;
            this.CheckModUpdate.Click += new System.EventHandler(this.CheckModUpdate_Click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // Dummy
            // 
            this.Dummy.Location = new System.Drawing.Point(0, 0);
            this.Dummy.Name = "Dummy";
            this.Dummy.Size = new System.Drawing.Size(0, 0);
            this.Dummy.TabIndex = 8;
            // 
            // ModListProgressBar
            // 
            this.ModListProgressBar.Location = new System.Drawing.Point(118, 108);
            this.ModListProgressBar.MarqueeAnimationSpeed = 1;
            this.ModListProgressBar.Name = "ModListProgressBar";
            this.ModListProgressBar.Size = new System.Drawing.Size(175, 14);
            this.ModListProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ModListProgressBar.TabIndex = 2;
            this.ModListProgressBar.Visible = false;
            // 
            // FileSystemWatcher
            // 
            this.FileSystemWatcher.SynchronizingObject = this;
            this.FileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.FileSystemWatcher_Changed);
            // 
            // ModCount
            // 
            this.ModCount.Location = new System.Drawing.Point(366, 5);
            this.ModCount.Name = "ModCount";
            this.ModCount.Size = new System.Drawing.Size(41, 15);
            this.ModCount.TabIndex = 3;
            this.ModCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.Dummy);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.CheckModUpdate);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MCModSync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.FocusToDummy);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileSystemWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox RepositoryUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox ModDirectoryPath;
        private System.Windows.Forms.ComboBox MinecraftProfileList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckedListBox LocalModList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CheckModUpdate;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.Label Dummy;
        private System.Windows.Forms.ProgressBar ModListProgressBar;
        private System.IO.FileSystemWatcher FileSystemWatcher;
        private System.Windows.Forms.Label ModCount;
    }
}

