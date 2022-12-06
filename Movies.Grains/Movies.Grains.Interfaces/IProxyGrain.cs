using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	public interface IProxyGrain
	{
		Task<IMovieGrain> GetMovieGrainAsync(int id);
	}
}
