using System.Threading.Tasks;

namespace Movies.Server.DataSetup
{
	public interface IDataHelper
	{
		Task PreLoadRedisAsync();
		Task SaveRedisDataAsync();
	}
}
