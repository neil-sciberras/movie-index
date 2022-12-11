using System.IO;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public class FileRepository : IFileRepository
	{
		public async Task<string> GetContentsAsync(FileOptions fileOptions)
		{
			var fileInfo = FileInfoHelper.GetFileInfo(fileOptions);

			using (var stream = fileInfo.OpenText())
			{
				return await stream.ReadToEndAsync();
			}
		}

		public async Task WriteContentsAsync(FileOptions fileOptions, string contents)
		{
			var fileInfo = FileInfoHelper.GetFileInfo(fileOptions);

			using (var streamWriter = new StreamWriter(fileInfo.Open(FileMode.Create, FileAccess.Write)))
			{
				await streamWriter.WriteAsync(contents);
			}
		}
	}
}
