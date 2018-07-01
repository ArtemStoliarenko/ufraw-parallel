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
	public static class UfrawMultithreadConverter
	{
		private const string ufrawBatchLocationCofnigKey = "UfrawBatchLocation";

		private static readonly Lazy<string> ufrawBatchExeLocation = new Lazy<string>(() => ConfigurationManager.AppSettings[ufrawBatchLocationCofnigKey]);

		private const string outTypeArgumentHeader = "--out-type ";

		private static readonly Dictionary<ImageFormat, string> imageFormatExtensions = new Dictionary<ImageFormat, string>()
		{
			{ ImageFormat.Png, "png" },
			{ ImageFormat.Jpeg, "jpg" },
			{ ImageFormat.Tiff, "tiff" },
			{ ImageFormat.Ppm, "ppm" }
		};

		private const char filenameParameterDelimeter = ' ';

		/// <summary>
		/// Converts all non-converted files in directory to the selected format.
		/// </summary>
		/// <param name="directory">Directory to be processed.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances, <see cref="Environment.ProcessorCount"/> will be selected if null.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		public static string Convert(string directory, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null) =>
			ConvertAsync(directory, imageFormat, maxThreads, handlers).Result;

		/// <summary>
		/// Converts all non-converted files in directory to the selected format.
		/// </summary>
		/// <param name="filenamesToConvert">Filenames of files to be converted.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances, <see cref="Environment.ProcessorCount"/> will be selected if null.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		public static string Convert(string[] filenamesToConvert, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null) =>
			Convert(filenamesToConvert, imageFormat, maxThreads, handlers);

		/// <summary>
		/// Converts all non-converted files in directory to the selected format asynchronously.
		/// </summary>
		/// <param name="directory">Directory to be processed.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances, <see cref="Environment.ProcessorCount"/> will be selected if null.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		/// <exception cref="ArgumentException">Wrong image format.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Max threads is negative or zero.</exception>
		public static async Task<string> ConvertAsync(string directory, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null)
		{
			CheckCommonPrerequisites(imageFormat, ref maxThreads);

			if (!Directory.Exists(directory))
				throw new ArgumentException(nameof(directory));

			string outTypeParameter = imageFormatExtensions[imageFormat];
			string[] filteredFilenames = FileFilter.GetFilenamesToConvert(directory, outTypeParameter);

			var runResults = await ConvertInner(filteredFilenames, outTypeParameter, maxThreads.Value, handlers);
			return runResults.CombinedOutput;
		}

		/// <summary>
		/// Converts all non-converted files in directory to the selected format asynchronously.
		/// </summary>
		/// <param name="filenamesToConvert">Filenames of files to be converted.</param>
		/// <param name="imageFormat">Chosen image format.</param>
		/// <param name="maxThreads">Max amount of running parallel instances, <see cref="Environment.ProcessorCount"/> will be selected if null.</param>
		/// <param name="handlers">Handlers of the ufraw-batch console output.</param>
		/// <returns>Output of all ufraw-batch instances.</returns>
		public static async Task<string> ConvertAsync(string[] filenamesToConvert, ImageFormat imageFormat, int? maxThreads = null, UfrawOutputHandlers handlers = null)
		{
			CheckCommonPrerequisites(imageFormat, ref maxThreads);

			if (filenamesToConvert == null)
				throw new ArgumentNullException(nameof(filenamesToConvert));

			string outTypeParameter = imageFormatExtensions[imageFormat];
			string[] filteredFilenames = FileFilter.GetFilenamesToConvert(filenamesToConvert, outTypeParameter);

			var runResults = await ConvertInner(filteredFilenames, outTypeParameter, maxThreads.Value, handlers);
			return runResults.CombinedOutput;
		}

		private static void CheckCommonPrerequisites(ImageFormat imageFormat, ref int? maxThreads)
		{
			if (imageFormat == ImageFormat.None)
				throw new ArgumentException(nameof(imageFormat));

			if (maxThreads <= 0)
				throw new ArgumentOutOfRangeException(nameof(maxThreads));

			if (!maxThreads.HasValue)
				maxThreads = Environment.ProcessorCount;
		}

		private static Task<RunResults> ConvertInner(string[] filteredFilenames, string outTypeParameter, int maxThreads, UfrawOutputHandlers handlers)
		{
			string[] arguments = PrepareArguments(filteredFilenames, outTypeParameter, maxThreads);
			return StartConversion(arguments, handlers);
		}

		private static string[] PrepareArguments(string[] filenamesToConvert, string outTypeParameter, int maxThreads)
		{
			string initValue = $"{outTypeArgumentHeader} {outTypeParameter}";

			int taskCount = Math.Min(maxThreads, filenamesToConvert.Length);
			var outputParameters = new StringBuilder[taskCount];
			for (int i = 0; i < taskCount; ++i)
				outputParameters[i] = new StringBuilder(initValue);

			for (int i = 0; i < filenamesToConvert.Length; ++i)
			{
				int currentIndex = i % taskCount;
				outputParameters[currentIndex].Append(filenameParameterDelimeter);
				outputParameters[currentIndex].Append(filenamesToConvert[i]);
			}

			return outputParameters.Select(sb => sb.ToString())
				.ToArray();
		}

		private static async Task<RunResults> StartConversion(string[] outputArguments, UfrawOutputHandlers handlers)
		{
			var messageFormatter = GetMessageFormatter(handlers);
			using (var ufrawRunner = new ProcessRunner(ufrawBatchExeLocation.Value, outputArguments, false, messageFormatter))
			{
				SetHandlersIfNecessary(ufrawRunner, handlers);
				var runResults = await ufrawRunner.StartAsync();
				return runResults;
			}
		}

		private static IMessageFormatter GetMessageFormatter(UfrawOutputHandlers handlers) =>
			handlers != null && handlers.AnyHandlerSet ? MessageFormatter.AppendLine : MessageFormatter.NoMessages;

		private static void SetHandlersIfNecessary(ProcessRunner ufrawRunner, UfrawOutputHandlers handlers)
		{
			if (handlers != null && handlers.AnyHandlerSet)
			{
				ufrawRunner.OutputChanged += handlers.OutputChangedHandler;
				ufrawRunner.ErrorChanged += handlers.ErrorChangedHandler;
				ufrawRunner.CombinedOutputChanged += handlers.CombinedOutputChangedHandler;
			}
		}
	}
}
