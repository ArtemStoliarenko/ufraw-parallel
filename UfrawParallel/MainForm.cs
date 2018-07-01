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
		private const string showButtonText = "Show output";

		private const string collapseButtonTest = "Collapse output";

		private const string successMessage = "Conversion successful!";

		private const string errorMessage = "Error happened: ";

		private const int collapsedHeight = 130;

		private const int uncollapsedHeight = 290;

		public MainForm()
		{
			InitializeComponent();
			cbFormat.SelectedIndex = 0;
		}

		private async void btnConvertFiles_Click(object sender, EventArgs e)
		{
			try
			{
				var dialogResult = ofd.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					var imageFormat = GetImageFormat();
					var filenames = ofd.FileNames;
					var handlers = new UfrawOutputHandlers(null, null, CombinedOutputChangedHandler);

					ChangeButtonsState(false);
					tbOutput.Text = await UfrawMultithreadConverter.ConvertAsync(filenames, imageFormat, null, handlers);
					MessageBox.Show(successMessage);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(errorMessage + ex.Message);
			}
			finally
			{
				ChangeButtonsState(true);
			}
		}

		private async void btnConvertFolder_Click(object sender, EventArgs e)
		{
			try
			{
				var dialogResult = fbd.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					var imageFormat = GetImageFormat();
					var folder = fbd.SelectedPath;
					var handlers = new UfrawOutputHandlers(null, null, CombinedOutputChangedHandler);

					ChangeButtonsState(false);
					tbOutput.Text = await UfrawMultithreadConverter.ConvertAsync(folder, imageFormat, null, handlers);
					MessageBox.Show(successMessage);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(errorMessage + ex.Message);
			}
			finally
			{
				ChangeButtonsState(true);
			}
		}

		private void btnOutput_Click(object sender, EventArgs e)
		{
			try
			{
				bool collapsed = spMainForm.Panel2Collapsed;

				if (collapsed)
				{
					spMainForm.Panel2Collapsed = false;
					btnOutput.Text = collapseButtonTest;

					this.Height = uncollapsedHeight;
				}
				else
				{
					spMainForm.Panel2Collapsed = true;
					btnOutput.Text = showButtonText;

					this.Height = collapsedHeight;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(errorMessage + ex.Message);
			}
		}

		private ImageFormat GetImageFormat() => (ImageFormat)Enum.Parse(typeof(ImageFormat), (string)cbFormat.SelectedItem);

		private void ChangeButtonsState(bool state)
		{
			btnConvertFiles.Enabled = state;
			btnConvertFolder.Enabled = state;
		}

		private void CombinedOutputChangedHandler(object sender, string newOutput)
		{
			tbOutput.Invoke((MethodInvoker)(() => tbOutput.Text = newOutput));
		}

	}
}
