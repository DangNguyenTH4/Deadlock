namespace Deadlock_8
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.txtResouceHave = new System.Windows.Forms.TextBox();
            this.txtResourceWant = new System.Windows.Forms.TextBox();
            this.btnAddProcess = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnFindDeadlock = new System.Windows.Forms.Button();
            this.btnVe = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnTakeDataFromFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnvetientrinh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tiến trình";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tài nguyên sở hữu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tài nguyên mong muốn";
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(263, 13);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(301, 20);
            this.txtProcess.TabIndex = 0;
            // 
            // txtResouceHave
            // 
            this.txtResouceHave.Location = new System.Drawing.Point(263, 54);
            this.txtResouceHave.Name = "txtResouceHave";
            this.txtResouceHave.Size = new System.Drawing.Size(301, 20);
            this.txtResouceHave.TabIndex = 1;
            // 
            // txtResourceWant
            // 
            this.txtResourceWant.Location = new System.Drawing.Point(263, 86);
            this.txtResourceWant.Name = "txtResourceWant";
            this.txtResourceWant.Size = new System.Drawing.Size(301, 20);
            this.txtResourceWant.TabIndex = 2;
            // 
            // btnAddProcess
            // 
            this.btnAddProcess.Location = new System.Drawing.Point(570, 8);
            this.btnAddProcess.Name = "btnAddProcess";
            this.btnAddProcess.Size = new System.Drawing.Size(75, 23);
            this.btnAddProcess.TabIndex = 3;
            this.btnAddProcess.Text = "Thêm";
            this.btnAddProcess.UseVisualStyleBackColor = true;
            this.btnAddProcess.Click += new System.EventHandler(this.btnAddProcess_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(570, 84);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnFindDeadlock
            // 
            this.btnFindDeadlock.Location = new System.Drawing.Point(651, 8);
            this.btnFindDeadlock.Name = "btnFindDeadlock";
            this.btnFindDeadlock.Size = new System.Drawing.Size(110, 98);
            this.btnFindDeadlock.TabIndex = 4;
            this.btnFindDeadlock.Text = "Tìm deadlock";
            this.btnFindDeadlock.UseVisualStyleBackColor = true;
            this.btnFindDeadlock.Click += new System.EventHandler(this.btnFindDeadlock_Click);
            // 
            // btnVe
            // 
            this.btnVe.Location = new System.Drawing.Point(86, 193);
            this.btnVe.Name = "btnVe";
            this.btnVe.Size = new System.Drawing.Size(75, 24);
            this.btnVe.TabIndex = 6;
            this.btnVe.Text = "Vẽ thủ công";
            this.btnVe.UseVisualStyleBackColor = true;
            this.btnVe.Click += new System.EventHandler(this.btnVe_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(86, 223);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(675, 517);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // btnTakeDataFromFile
            // 
            this.btnTakeDataFromFile.Location = new System.Drawing.Point(570, 129);
            this.btnTakeDataFromFile.Name = "btnTakeDataFromFile";
            this.btnTakeDataFromFile.Size = new System.Drawing.Size(161, 23);
            this.btnTakeDataFromFile.TabIndex = 8;
            this.btnTakeDataFromFile.Text = "Lấy dữ liệu từ file...";
            this.btnTakeDataFromFile.UseVisualStyleBackColor = true;
            this.btnTakeDataFromFile.Click += new System.EventHandler(this.btnTakeDataFromFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(263, 132);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(301, 20);
            this.txtFileName.TabIndex = 9;
            // 
            // btnvetientrinh
            // 
            this.btnvetientrinh.Location = new System.Drawing.Point(179, 194);
            this.btnvetientrinh.Name = "btnvetientrinh";
            this.btnvetientrinh.Size = new System.Drawing.Size(180, 23);
            this.btnvetientrinh.TabIndex = 10;
            this.btnvetientrinh.Text = " Vẽ các tiến trình gây ra deadlock";
            this.btnvetientrinh.UseVisualStyleBackColor = true;
            this.btnvetientrinh.Click += new System.EventHandler(this.btnvetientrinh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 808);
            this.Controls.Add(this.btnvetientrinh);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnTakeDataFromFile);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnVe);
            this.Controls.Add(this.btnFindDeadlock);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnAddProcess);
            this.Controls.Add(this.txtResourceWant);
            this.Controls.Add(this.txtResouceHave);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.TextBox txtResouceHave;
        private System.Windows.Forms.TextBox txtResourceWant;
        private System.Windows.Forms.Button btnAddProcess;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnFindDeadlock;
        private System.Windows.Forms.Button btnVe;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnTakeDataFromFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnvetientrinh;
    }
}

