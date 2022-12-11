using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public interface IFileRepository
	{
		Task<string> GetContentsAsync(FileOptions fileOptions);
		Task WriteContentsAsync(FileOptions fileOptions, string contents);
	}
}
