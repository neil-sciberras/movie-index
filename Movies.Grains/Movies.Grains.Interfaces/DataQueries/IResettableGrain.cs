using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	public interface IResettableGrain : IGrain
	{
		Task ResetStateAsync();
	}
}
