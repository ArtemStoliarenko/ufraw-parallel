using System;
using System.IO;

namespace UfrawParallel.Core
{
	/// <summary>
	/// This class contains filename details.
	/// </summary>
	sealed class FilenameDetails
	{
		/// <summary>
		/// The filename without extension.
		/// </summary>
		public string FilenameWithoutExtension { get; }

		/// <summary>
		/// The file extension.
		/// </summary>
		public string Extension { get; }

		/// <summary>
		/// The filename.
		/// </summary>
		public string Filename { get; }

		/// <summary>
		/// Gets the object hash code.
		/// </summary>
		/// <returns>Object hash code.</returns>
		/// <remarks>Returns hash code of the <see cref="Filename"/> property.</remarks>
		public override int GetHashCode()
		{
			return Filename.GetHashCode();
		}

		/// <summary>
		/// Creates the <see cref="FilenameDetails"/> instance.
		/// </summary>
		/// <param name="filename">Filename to be processed.</param>
		/// <returns><see cref="FilenameDetails"/> instance if creation was succesful; otherwise, null.</returns>
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
