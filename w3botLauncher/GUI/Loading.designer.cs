namespace w3botLauncher.GUI
{
    partial class Loading
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadingLabel = new System.Windows.Forms.Label();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusLabelMessage = new System.Windows.Forms.Label();
            this.fileDataReceivedLabel = new System.Windows.Forms.Label();
            this.LoadingBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.LoadingFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Location = new System.Drawing.Point(58, 9);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(146, 13);
            this.loadingLabel.TabIndex = 0;
            this.loadingLabel.Text = "Loading w3bot. Please wait...";
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Location = new System.Drawing.Point(15, 25);
            this.downloadProgressBar.MarqueeAnimationSpeed = 50;
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(221, 23);
            this.downloadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.downloadProgressBar.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 53);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Status:";
            // 
            // statusLabelMessage
            // 
            this.statusLabelMessage.AutoSize = true;
            this.statusLabelMessage.Location = new System.Drawing.Point(49, 53);
            this.statusLabelMessage.Name = "statusLabelMessage";
            this.statusLabelMessage.Size = new System.Drawing.Size(35, 13);
            this.statusLabelMessage.TabIndex = 3;
            this.statusLabelMessage.Text = "NULL";
            // 
            // fileDataReceivedLabel
            // 
            this.fileDataReceivedLabel.Location = new System.Drawing.Point(156, 53);
            this.fileDataReceivedLabel.Name = "fileDataReceivedLabel";
            this.fileDataReceivedLabel.Size = new System.Drawing.Size(80, 13);
            this.fileDataReceivedLabel.TabIndex = 4;
            this.fileDataReceivedLabel.Text = "NULL";
            this.fileDataReceivedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LoadingBackgroundWorker
            // 
            this.LoadingBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadingBackgroundWorker_DoWork);
            this.LoadingBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.LoadingBackgroundWorker_ProgressChanged);
            this.LoadingBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadingBackgroundWorker_RunWorkerCompleted);
            // 
            // LoadingFolderBrowserDialog
            // 
            this.LoadingFolderBrowserDialog.Description = "Select your install directory for w3bot.";
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 75);
            this.ControlBox = false;
            this.Controls.Add(this.fileDataReceivedLabel);
            this.Controls.Add(this.statusLabelMessage);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.loadingLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loading";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Loading_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label statusLabelMessage;
        private System.Windows.Forms.Label fileDataReceivedLabel;
        private System.ComponentModel.BackgroundWorker LoadingBackgroundWorker;
        private System.Windows.Forms.FolderBrowserDialog LoadingFolderBrowserDialog;
    }
}