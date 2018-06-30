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
			this.SuspendLayout();
			// 
			// lbFormat
			// 
			this.lbFormat.AutoSize = true;
			this.lbFormat.Location = new System.Drawing.Point(13, 13);
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
			this.cbFormat.Location = new System.Drawing.Point(62, 13);
			this.cbFormat.Name = "cbFormat";
			this.cbFormat.Size = new System.Drawing.Size(121, 21);
			this.cbFormat.TabIndex = 1;
			// 
			// btnConvertFiles
			// 
			this.btnConvertFiles.Location = new System.Drawing.Point(13, 41);
			this.btnConvertFiles.Name = "btnConvertFiles";
			this.btnConvertFiles.Size = new System.Drawing.Size(170, 23);
			this.btnConvertFiles.TabIndex = 2;
			this.btnConvertFiles.Text = "Convert file(s)";
			this.btnConvertFiles.UseVisualStyleBackColor = true;
			// 
			// btnConvertFolder
			// 
			this.btnConvertFolder.Location = new System.Drawing.Point(13, 71);
			this.btnConvertFolder.Name = "btnConvertFolder";
			this.btnConvertFolder.Size = new System.Drawing.Size(170, 23);
			this.btnConvertFolder.TabIndex = 3;
			this.btnConvertFolder.Text = "Convert Folder";
			this.btnConvertFolder.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(204, 261);
			this.Controls.Add(this.btnConvertFolder);
			this.Controls.Add(this.btnConvertFiles);
			this.Controls.Add(this.cbFormat);
			this.Controls.Add(this.lbFormat);
			this.Name = "MainForm";
			this.Text = "UfrawParallel";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.ComboBox cbFormat;
		private System.Windows.Forms.Button btnConvertFiles;
		private System.Windows.Forms.Button btnConvertFolder;
	}
}

