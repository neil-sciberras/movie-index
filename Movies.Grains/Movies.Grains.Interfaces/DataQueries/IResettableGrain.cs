using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.DataQueries
{
	public interface IResettableGrain : IGrainWithIntegerKey
	{
		Task ResetStateAsync();
	}
}
