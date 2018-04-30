using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UfrawParallel.Core
{
    static class FileFilter
    {
		private static readonly HashSet<string> convertedFileExtensions =
			new HashSet<string> { "png", "jpg", "jpeg", "tif", "tiff", "ppm" };

		public static string[] GetFilenamesToConvert(string[] sourceFilenames)
		{
			var filenameDetails = sourceFilenames.Select(filename => FilenameDetails.Create(filename))
				.Where(fd => fd != null);
			var convertedFilenamesEnuumberable = filenameDetails.Where(fd => IsConvertedFileExtension(fd.Extension))
				.Select(fd => fd.FilenameWithoutExtension);
			var convertedFilenamesSet = new HashSet<string>(convertedFilenamesEnuumberable);

			return filenameDetails.Where(fd => !IsConvertedFileExtension(fd.Extension) && !convertedFilenamesSet.Contains(fd.FilenameWithoutExtension))
				.Select(fd => fd.Filename)
				.ToArray();
		}

		private static bool IsConvertedFileExtension(string extension) => convertedFileExtensions.Contains(extension);
    }
}
