using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.GrainClients
{
	public interface ISampleGrainClient
	{
		Task<SampleDataModel> Get(string id);
		Task Set(string key, string name);
	}
}
