using System.Collections.Generic;

namespace Movies.Grains.Supervisors
{
	public class SupervisorState
	{
		public HashSet<int> SupervisedGrainsPrimaryKeys { get; set; }
	}
}
