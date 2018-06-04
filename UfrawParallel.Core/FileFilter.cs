using System;
using System.Collections.Generic;
using System.Linq;

namespace UfrawParallel.Core
{
	/// <summary>
	/// This class implements file filtering logic.
	/// </summary>
	static class FileFilter
	{
		/// <summary>
		/// Filters the array of filenames, leaving only non-converted to the chosen format.
		/// </summary>
		/// <param name="sourceFilenames">Source array of filenames.</param>
		/// <param name="convertedExtension"></param>
		/// <returns></returns>
		public static string[] GetFilenamesToConvert(string[] sourceFilenames, string convertedExtension)
		{
			var filenameDetails = sourceFilenames.Select(filename => FilenameDetails.Create(filename))
				.Where(fd => fd != null);
			var convertedFilenamesEnuumberable = filenameDetails.Where(fd => !CompareExtensions(fd.Extension, convertedExtension))
				.Select(fd => fd.FilenameWithoutExtension);
			var convertedFilenamesSet = new HashSet<string>(convertedFilenamesEnuumberable);

			return filenameDetails.Where(fd => !convertedFilenamesSet.Contains(fd.FilenameWithoutExtension))
				.Select(fd => fd.Filename)
				.ToArray();
		}

		private static bool CompareExtensions(string extensions1, string extension2)
		{
			return extensions1.Equals(extension2, StringComparison.OrdinalIgnoreCase);
		}
	}
}
