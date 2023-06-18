namespace PublicRelations.SocialMedia.Youtube
{
    partial class MyYoutubeUploaded
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
            this.youtubeUploadedData = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.ModifyRecord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.youtubeUploadedData)).BeginInit();
            this.SuspendLayout();
            // 
            // youtubeUploadedData
            // 
            this.youtubeUploadedData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.youtubeUploadedData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.youtubeUploadedData.Location = new System.Drawing.Point(87, 68);
            this.youtubeUploadedData.Name = "youtubeUploadedData";
            this.youtubeUploadedData.RowHeadersWidth = 51;
            this.youtubeUploadedData.RowTemplate.Height = 24;
            this.youtubeUploadedData.Size = new System.Drawing.Size(500, 211);
            this.youtubeUploadedData.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Magneto", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(124, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(383, 41);
            this.label6.TabIndex = 4;
            this.label6.Text = "My Youtube Uploads";
            // 
            // ModifyRecord
            // 
            this.ModifyRecord.Location = new System.Drawing.Point(87, 314);
            this.ModifyRecord.Name = "ModifyRecord";
            this.ModifyRecord.Size = new System.Drawing.Size(75, 23);
            this.ModifyRecord.TabIndex = 5;
            this.ModifyRecord.Text = "Modify";
            this.ModifyRecord.UseVisualStyleBackColor = true;
            this.ModifyRecord.Click += new System.EventHandler(this.ModifyRecord_Click);
            // 
            // MyYoutubeUploaded
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ModifyRecord);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.youtubeUploadedData);
            this.Name = "MyYoutubeUploaded";
            this.Text = "My Youtube Upload";
            this.Load += new System.EventHandler(this.MyYoutubeUploaded_Load);
            ((System.ComponentModel.ISupportInitialize)(this.youtubeUploadedData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView youtubeUploadedData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ModifyRecord;
    }
}