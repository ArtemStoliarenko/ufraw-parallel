using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UfrawParallel.Core
{
	/// <summary>
	/// This class implements file filtering logic.
	/// </summary>
	static class FileFilter
	{
		/// <summary>
		/// Filters all specified filenames, leaving only unconverted.
		/// </summary>
		/// <param name="sourceFilenames">Array of filenames to be filtered.</param>
		/// <param name="convertedExtension">Extension of the converted file</param>
		/// <returns>Array of filenames of the files to  be converted.</returns>
		public static string[] GetFilenamesToConvert(string[] sourceFilenames, string convertedExtension) =>
			sourceFilenames.Where(filename => !ConvertedFileExistsInTheSameFolder(filename, convertedExtension)).ToArray();

		/// <summary>
		/// Filters all files in the specified directory, leaving only non-converted to the chosen format.
		/// </summary>
		/// <param name="directory">Directory to get filenames from.</param>
		/// <param name="convertedExtension">Extension of the converted file.</param>
		/// <returns>Array of filenames of the files to  be converted.</returns>
		public static string[] GetFilenamesToConvert(string directory, string convertedExtension)
		{
			var sourceFilenames = Directory.GetFiles(directory);
			var filenameDetails = sourceFilenames.Select(filename => FilenameDetails.Create(filename))
				.Where(fd => fd != null);
			var convertedFilenamesEnuumberable = filenameDetails.Where(fd => CompareExtensions(fd.Extension, convertedExtension))
				.Select(fd => fd.FilenameWithoutExtension);
			var convertedFilenamesSet = new HashSet<string>(convertedFilenamesEnuumberable);

			return filenameDetails.Where(fd => !convertedFilenamesSet.Contains(fd.FilenameWithoutExtension))
				.Select(fd => fd.Filename)
				.ToArray();
		}

		private static bool ConvertedFileExistsInTheSameFolder(string sourceFilename, string convertedExtension)
		{
			string convertedFilename = Path.ChangeExtension(sourceFilename, convertedExtension);
			return File.Exists(convertedFilename);
		}

		private static bool CompareExtensions(string extensions1, string extension2)
		{
			return extensions1.Equals(extension2, StringComparison.OrdinalIgnoreCase);
		}
	}
}
