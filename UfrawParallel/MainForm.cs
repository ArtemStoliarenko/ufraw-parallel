using System;
using System.Windows.Forms;
using UfrawParallel.Core;

namespace UfrawParallel
{
	public partial class MainForm : Form
	{
		private const string showButtonText = "Show output";

		private const string collapseButtonTest = "Collapse output";

		private const string errorMessage = "Error happened: ";

		private const int collapsedHeight = 130;

		private const int uncollapsedHeight = 290;

		private const int virtualProcessorsPerReal = 2;

		private const int minimumAmountOfProcess = 1;

		private readonly UfrawMultithreadConverter multithreadConverter = new UfrawMultithreadConverter();

		public MainForm()
		{
			InitializeComponent();

			cbFormat.SelectedIndex = 0;

			nudThreads.Value = Math.Max(minimumAmountOfProcess, Environment.ProcessorCount / virtualProcessorsPerReal);
			nudThreads.Maximum = Environment.ProcessorCount;
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

					ChangeButtonsState(true);
					await multithreadConverter.ConvertAsync(filenames, imageFormat, Convert.ToInt32(nudThreads.Value), handlers);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(errorMessage + ex.Message);
			}
			finally
			{
				ChangeButtonsState(false);
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

					ChangeButtonsState(true);
					await multithreadConverter.ConvertAsync(folder, imageFormat, Convert.ToInt32(nudThreads.Value), handlers);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(errorMessage + ex.Message);
			}
			finally
			{
				ChangeButtonsState(false);
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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			multithreadConverter.CancelConversion();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			multithreadConverter.CancelConversion();
		}

		private ImageFormat GetImageFormat() => (ImageFormat)Enum.Parse(typeof(ImageFormat), (string)cbFormat.SelectedItem);

		private void ChangeButtonsState(bool conversionRunning)
		{
			btnConvertFiles.Enabled = !conversionRunning;
			btnConvertFolder.Enabled = !conversionRunning;

			btnCancel.Enabled = conversionRunning;
		}

		private void CombinedOutputChangedHandler(object sender, string newOutput)
		{
			tbOutput.Invoke((MethodInvoker)(() =>
			{
				tbOutput.AppendText(newOutput);
				if (!newOutput.EndsWith(Environment.NewLine))
					tbOutput.AppendText(Environment.NewLine);
			}));
		}

	}
}
