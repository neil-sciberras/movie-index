using System.IO;

namespace Movies.Infrastructure.File
{
	public static class FileInfoHelper
	{
		public static FileInfo GetFileInfo(FileOptions fileOptions)
		{
			var fileInfo = new FileInfo(fileOptions.FullFileName);

			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException($"File '{fileInfo.FullName}' does not exist");
			}

			return fileInfo;
		}
	}
}
