using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface ISampleGrainClient
	{
		Task<SampleDataModel> Get(string id);
		Task Set(string key, string name);
	}
}
