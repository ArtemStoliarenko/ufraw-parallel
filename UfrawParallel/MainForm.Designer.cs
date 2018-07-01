namespace UfrawParallel
{
	partial class MainForm
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
			this.lbFormat = new System.Windows.Forms.Label();
			this.cbFormat = new System.Windows.Forms.ComboBox();
			this.btnConvertFiles = new System.Windows.Forms.Button();
			this.btnConvertFolder = new System.Windows.Forms.Button();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.spMainForm = new System.Windows.Forms.SplitContainer();
			this.btnOutput = new System.Windows.Forms.Button();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.lbOutput = new System.Windows.Forms.Label();
			this.lbThreads = new System.Windows.Forms.Label();
			this.nudThreads = new System.Windows.Forms.NumericUpDown();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.spMainForm)).BeginInit();
			this.spMainForm.Panel1.SuspendLayout();
			this.spMainForm.Panel2.SuspendLayout();
			this.spMainForm.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudThreads)).BeginInit();
			this.SuspendLayout();
			// 
			// lbFormat
			// 
			this.lbFormat.AutoSize = true;
			this.lbFormat.Location = new System.Drawing.Point(3, 16);
			this.lbFormat.Name = "lbFormat";
			this.lbFormat.Size = new System.Drawing.Size(42, 13);
			this.lbFormat.TabIndex = 0;
			this.lbFormat.Text = "Format:";
			// 
			// cbFormat
			// 
			this.cbFormat.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFormat.FormattingEnabled = true;
			this.cbFormat.Items.AddRange(new object[] {
            "Png",
            "Jpeg",
            "Tiff",
            "Ppm"});
			this.cbFormat.Location = new System.Drawing.Point(51, 11);
			this.cbFormat.Name = "cbFormat";
			this.cbFormat.Size = new System.Drawing.Size(121, 21);
			this.cbFormat.TabIndex = 1;
			// 
			// btnConvertFiles
			// 
			this.btnConvertFiles.Location = new System.Drawing.Point(178, 11);
			this.btnConvertFiles.Name = "btnConvertFiles";
			this.btnConvertFiles.Size = new System.Drawing.Size(170, 23);
			this.btnConvertFiles.TabIndex = 2;
			this.btnConvertFiles.Text = "Convert file(s)";
			this.btnConvertFiles.UseVisualStyleBackColor = true;
			this.btnConvertFiles.Click += new System.EventHandler(this.btnConvertFiles_Click);
			// 
			// btnConvertFolder
			// 
			this.btnConvertFolder.Location = new System.Drawing.Point(354, 11);
			this.btnConvertFolder.Name = "btnConvertFolder";
			this.btnConvertFolder.Size = new System.Drawing.Size(170, 23);
			this.btnConvertFolder.TabIndex = 3;
			this.btnConvertFolder.Text = "Convert Folder";
			this.btnConvertFolder.UseVisualStyleBackColor = true;
			this.btnConvertFolder.Click += new System.EventHandler(this.btnConvertFolder_Click);
			// 
			// ofd
			// 
			this.ofd.FileName = "ofd";
			// 
			// spMainForm
			// 
			this.spMainForm.Location = new System.Drawing.Point(12, 12);
			this.spMainForm.Name = "spMainForm";
			this.spMainForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spMainForm.Panel1
			// 
			this.spMainForm.Panel1.Controls.Add(this.btnCancel);
			this.spMainForm.Panel1.Controls.Add(this.nudThreads);
			this.spMainForm.Panel1.Controls.Add(this.lbThreads);
			this.spMainForm.Panel1.Controls.Add(this.btnOutput);
			this.spMainForm.Panel1.Controls.Add(this.lbFormat);
			this.spMainForm.Panel1.Controls.Add(this.btnConvertFolder);
			this.spMainForm.Panel1.Controls.Add(this.cbFormat);
			this.spMainForm.Panel1.Controls.Add(this.btnConvertFiles);
			// 
			// spMainForm.Panel2
			// 
			this.spMainForm.Panel2.Controls.Add(this.tbOutput);
			this.spMainForm.Panel2.Controls.Add(this.lbOutput);
			this.spMainForm.Panel2Collapsed = true;
			this.spMainForm.Size = new System.Drawing.Size(544, 248);
			this.spMainForm.SplitterDistance = 69;
			this.spMainForm.TabIndex = 4;
			// 
			// btnOutput
			// 
			this.btnOutput.Location = new System.Drawing.Point(178, 41);
			this.btnOutput.Name = "btnOutput";
			this.btnOutput.Size = new System.Drawing.Size(170, 23);
			this.btnOutput.TabIndex = 4;
			this.btnOutput.Text = "Show output";
			this.btnOutput.UseVisualStyleBackColor = true;
			this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
			// 
			// tbOutput
			// 
			this.tbOutput.Location = new System.Drawing.Point(6, 29);
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ReadOnly = true;
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(532, 115);
			this.tbOutput.TabIndex = 2;
			// 
			// lbOutput
			// 
			this.lbOutput.AutoSize = true;
			this.lbOutput.Location = new System.Drawing.Point(260, 13);
			this.lbOutput.Name = "lbOutput";
			this.lbOutput.Size = new System.Drawing.Size(42, 13);
			this.lbOutput.TabIndex = 0;
			this.lbOutput.Text = "Output:";
			// 
			// lbThreads
			// 
			this.lbThreads.AutoSize = true;
			this.lbThreads.Location = new System.Drawing.Point(3, 44);
			this.lbThreads.Name = "lbThreads";
			this.lbThreads.Size = new System.Drawing.Size(49, 13);
			this.lbThreads.TabIndex = 5;
			this.lbThreads.Text = "Threads:";
			// 
			// nudThreads
			// 
			this.nudThreads.Location = new System.Drawing.Point(51, 44);
			this.nudThreads.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.nudThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudThreads.Name = "nudThreads";
			this.nudThreads.Size = new System.Drawing.Size(120, 20);
			this.nudThreads.TabIndex = 6;
			this.nudThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnCancel
			// 
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(355, 41);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(169, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel conversion";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 91);
			this.Controls.Add(this.spMainForm);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "UfrawParallel";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.spMainForm.Panel1.ResumeLayout(false);
			this.spMainForm.Panel1.PerformLayout();
			this.spMainForm.Panel2.ResumeLayout(false);
			this.spMainForm.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.spMainForm)).EndInit();
			this.spMainForm.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudThreads)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.ComboBox cbFormat;
		private System.Windows.Forms.Button btnConvertFiles;
		private System.Windows.Forms.Button btnConvertFolder;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.FolderBrowserDialog fbd;
		private System.Windows.Forms.SplitContainer spMainForm;
		private System.Windows.Forms.Label lbOutput;
		private System.Windows.Forms.Button btnOutput;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.NumericUpDown nudThreads;
		private System.Windows.Forms.Label lbThreads;
		private System.Windows.Forms.Button btnCancel;
	}
}

