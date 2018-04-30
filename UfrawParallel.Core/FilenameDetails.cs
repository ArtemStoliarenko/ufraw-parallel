using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UfrawParallel.Core
{
    sealed class FilenameDetails
    {
		public string FilenameWithoutExtension { get; }

		public string Extension { get; }

		public string Filename { get; }

		public override bool Equals(object obj)
		{
			if (obj is FilenameDetails objFilenameDetails)
				return Filename.Equals(objFilenameDetails.Filename, StringComparison.OrdinalIgnoreCase);
			else
				return false;
		}

		public override int GetHashCode()
		{
			return Filename.GetHashCode();
		}

		public static FilenameDetails Create(string filename)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(filename))
					return null;

				return new FilenameDetails(Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename), filename);
			}
			catch (ArgumentException)
			{
				return null;
			}
		}

		private FilenameDetails(string filenameWithoutExtension, string extension, string filename)
		{
			if (string.IsNullOrEmpty(filenameWithoutExtension))
				throw new ArgumentException(nameof(filenameWithoutExtension));
			if (string.IsNullOrEmpty(extension))
				throw new ArgumentException(nameof(extension));
			if (string.IsNullOrWhiteSpace(filename))
				throw new ArgumentException(nameof(filename));

			this.FilenameWithoutExtension = filenameWithoutExtension;
			this.Extension = extension;
			this.Filename = filename;
		}
    }
}
