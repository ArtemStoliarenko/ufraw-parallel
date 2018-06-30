﻿using AsParallel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfrawParallel.Core
{
	/// <summary>
	/// Interacts with ufraw-batch to process multiple files simultaneously. 
	/// </summary>
	public class UfrawMultithreadConverter
	{
		private const string outTypeArgumentHeader = "--out-type ";

		private static readonly Dictionary<ImageFormat, string> imageFormatExtensions = new Dictionary<ImageFormat, string>()
		{
			{ ImageFormat.Png, "png" },
			{ ImageFormat.Jpeg, "jpg" },
			{ ImageFormat.Tiff, "tiff" },
			{ ImageFormat.Ppm, "ppm" }
		};

		private const char filenameParameterDelimeter = ' ';

		private readonly string ufrawBatchExeLocation = ConfigurationManager.AppSettings[ufrawBatchLocationCofnigKey];

		public static readonly string ufrawBatchLocationCofnigKey = "UfrawBatchLocation";

		/// <summary>
		/// Converts all non-converted files in directory to the selected format.
		/// </summary>
		/// <param name="directory">Directory to be processed.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		public string Convert(string directory, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null) =>
			ConvertAsync(directory, imageFormat, maxThreads, handlers).Result;

		/// <summary>
		/// Converts all non-converted files in directory to the selected format.
		/// </summary>
		/// <param name="filenamesToConvert">Filenames of files to be converted.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		public string Convert(string[] filenamesToConvert, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null) =>
			Convert(filenamesToConvert, imageFormat, maxThreads, handlers);

		/// <summary>
		/// Converts all non-converted files in directory to the selected format asynchronously.
		/// </summary>
		/// <param name="directory">Directory to be processed.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		/// <exception cref="ArgumentException">Wrong image format.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Max threads is negative or zero.</exception>
		public Task<string> ConvertAsync(string directory, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null)
		{
			if (!Directory.Exists(directory))
				throw new ArgumentException(nameof(directory));

			var filesToConvert = Directory.GetFiles(directory);
			return ConvertAsync(filesToConvert, imageFormat, maxThreads, handlers);
		}

		/// <summary>
		/// Converts all non-converted files in directory to the selected format asynchronously.
		/// </summary>
		/// <param name="filenamesToConvert">Filenames of files to be converted.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		public async Task<string> ConvertAsync(string[] filenamesToConvert, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null)
		{
			if (imageFormat == ImageFormat.None)
				throw new ArgumentException(nameof(imageFormat));

			if (maxThreads <= 0)
				throw new ArgumentOutOfRangeException(nameof(maxThreads));

			if (!maxThreads.HasValue)
				maxThreads = Environment.ProcessorCount;

			if (filenamesToConvert == null)
				throw new ArgumentNullException(nameof(filenamesToConvert));

			string[] outputArguments = GetOutputArguments(filenamesToConvert, imageFormat, maxThreads);
			var runResults = await Convert(outputArguments, handlers);
			return runResults.CombinedOutput;
		}

		private async Task<RunResults> Convert(string[] outputArguments, UfrawOutputHandlers handlers)
		{
			using (var ufrawRunner = new ProcessRunner(ufrawBatchExeLocation, outputArguments))
			{
				SetHandlersIfNecessary(ufrawRunner, handlers);
				var runResults = await ufrawRunner.StartAsync();
				return runResults;
			}
		}

		private static string[] GetOutputArguments(string[] filenames, ImageFormat imageFormat, int? maxThreads)
		{
			string outTypeArgument = imageFormatExtensions[imageFormat];
			var filenamesToConvert = FileFilter.GetFilenamesToConvert(filenames, outTypeArgument);
			var outputArguments = PrepareArguments(outTypeArgument, filenamesToConvert, maxThreads.Value);
			return outputArguments;
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

		private static void SetHandlersIfNecessary(ProcessRunner ufrawRunner, UfrawOutputHandlers handlers)
		{
			if (handlers != null)
			{
				ufrawRunner.OutputChanged += handlers.OutputChangedHandler;
				ufrawRunner.ErrorChanged += handlers.ErrorChangedHandler;
				ufrawRunner.CombinedOutputChanged += handlers.CombinedOutputChangedHandler;
			}
		}
	}
}
