using System.Threading.Tasks;

namespace Movies.Server.RedisBootstrap
{
	public interface IRedisBootstrapper
	{
		Task PreLoadRedisAsync();
		Task SaveRedisDataAsync();
	}
}
