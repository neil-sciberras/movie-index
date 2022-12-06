using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	public interface ISampleGrain : IGrainWithStringKey
	{
		Task<SampleDataModel> Get();
		Task Set(string name);
	}
}