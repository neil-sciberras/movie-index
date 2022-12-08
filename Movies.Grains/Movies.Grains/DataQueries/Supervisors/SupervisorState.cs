using System.Collections.Generic;

namespace Movies.Grains.DataQueries.Supervisors
{
	public class SupervisorState
	{
		public HashSet<int> SupervisedGrainsPrimaryKeys { get; set; }
	}
}
