namespace MCModSync {
    partial class RepositoryConfigDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.OKButton = new System.Windows.Forms.Button();
            this.RepositoryUrl = new System.Windows.Forms.TextBox();
            this.DialogMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(322, 100);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 30);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // RepositoryUrl
            // 
            this.RepositoryUrl.Location = new System.Drawing.Point(12, 67);
            this.RepositoryUrl.Name = "RepositoryUrl";
            this.RepositoryUrl.Size = new System.Drawing.Size(410, 23);
            this.RepositoryUrl.TabIndex = 4;
            // 
            // DialogMessage
            // 
            this.DialogMessage.Location = new System.Drawing.Point(12, 11);
            this.DialogMessage.Name = "DialogMessage";
            this.DialogMessage.Size = new System.Drawing.Size(410, 50);
            this.DialogMessage.TabIndex = 3;
            this.DialogMessage.Text = "リポジトリ URL を入力してください:";
            this.DialogMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RepositoryConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(434, 141);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RepositoryUrl);
            this.Controls.Add(this.DialogMessage);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepositoryConfigDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "リポジトリ設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox RepositoryUrl;
        private System.Windows.Forms.Label DialogMessage;
    }
}