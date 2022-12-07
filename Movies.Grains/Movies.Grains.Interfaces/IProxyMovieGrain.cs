using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	public interface IProxyMovieGrain
	{
		Task<IMovieGrain> GetMovieGrainAsync(int id);
	}
}
