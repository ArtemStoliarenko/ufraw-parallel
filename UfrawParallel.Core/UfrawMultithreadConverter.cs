using AsParallel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfrawParallel.Core
{
    public class UfrawMultithreadConverter
    {
		private const string ufrawBatchLocationCofnigKey = "UfrawBatchLocation";
		private static readonly Lazy<string> ufrawBatchExeLocation = new Lazy<string>(() => ConfigurationManager.AppSettings[ufrawBatchLocationCofnigKey]);

		private const string outTypeArgumentHeader = "--out-type ";
		private const string pngOutTypeArgumentValue = "png ";
		private const string jpegOutTypeArgumentValue = "jpg ";
		private const string tiffOutTypeArgumentValue = "tiff ";
		private const string ppmOutTypeArgumentValue = "ppm ";

		private const char filenameParameterDelimeter = ' ';

		public Task<string> ConvertAsync(string directory, ImageFormat imageFormat) => ConvertAsync(directory, imageFormat, Environment.ProcessorCount);

		public Task<string> ConvertAsync(string directory, ImageFormat imageFormat, int maxThreads)
		{
			if (maxThreads <= 0)
				throw new ArgumentOutOfRangeException(nameof(maxThreads));

			string outTypeArgument = GetOutTypeParameter(imageFormat);
			var filenamesToConvert = GetFilenamesToConvert(directory);
			var outputArguments = PrepareArguments(outTypeArgument, filenamesToConvert, maxThreads);

			throw new NotImplementedException();
		}

		private static string[] GetFilenamesToConvert(string directory)
		{
			return Directory.Exists(directory) ? FileFilter.GetFilenamesToConvert(Directory.GetFiles(directory))
				: new string[0];
		}

		private static string GetOutTypeParameter(ImageFormat imageFormat)
		{
			string result = outTypeArgumentHeader;
			switch (imageFormat)
			{
				case ImageFormat.Png:
					result += pngOutTypeArgumentValue;
					break;
				case ImageFormat.Jpeg:
					result += jpegOutTypeArgumentValue;
					break;
				case ImageFormat.Tiff:
					result += tiffOutTypeArgumentValue;
					break;
				case ImageFormat.Ppm:
					result += ppmOutTypeArgumentValue;
					break;
				default:
					throw new InvalidEnumArgumentException();
			}
			return result;
		}

		private static string[] PrepareArguments(string outTypeParameter, string[] filenamesToConvert, int maxThreads)
		{
			int taskCount = Math.Min(maxThreads, filenamesToConvert.Length);
			var outputParameters = new StringBuilder[taskCount];

			for (int i = 0; i < taskCount; ++i)
				outputParameters[i].Append(outTypeParameter);

			for (int i = 0; i < filenamesToConvert.Length; ++i)
			{
				int currentIndex = i % taskCount;
				outputParameters[currentIndex].Append(filenamesToConvert[i]);
				outputParameters[currentIndex].Append(filenameParameterDelimeter);
			}

			return outputParameters.Select(sb => sb.ToString())
				.ToArray();
		}
    }
}
