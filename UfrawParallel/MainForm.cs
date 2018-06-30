using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UfrawParallel.Core;

namespace UfrawParallel
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private ImageFormat GetImageFormat => (ImageFormat)Enum.Parse(typeof(ImageFormat), cbFormat.SelectedText);
	}
}
