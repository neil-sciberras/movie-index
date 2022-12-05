using Microsoft.Extensions.Logging;
using Movies.Extensions;
using Orleans;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

public class LoggingOutgoingCallFilter : IOutgoingGrainCallFilter
{
	private readonly ILogger _logger;

	public LoggingOutgoingCallFilter(ILogger<LoggingOutgoingCallFilter> logger)
	{
		_logger = logger;
	}

	public async Task Invoke(IOutgoingGrainCallContext context)
	{
		var grainType = context.Grain.GetType();
		var grainName = grainType.GetDemystifiedName();
		var shouldHaveDetailedTrace = grainType.Namespace.Contains("Movies"); // todo: Create log filter mechanism

		if (!shouldHaveDetailedTrace)
		{
			await context.Invoke();
			return;
		}

		string primaryKey = null;
		if (context.Grain is Grain grain)
			primaryKey = grain.GetPrimaryKeyAny();

		var stopwatch = Stopwatch.StartNew();

		try
		{
			await context.Invoke();
			stopwatch.Stop();

			//if (stopwatch.Elapsed > WarnThreshold)
			//{
			_logger.LogDebug(
				"Invoking grain method {grain}.{grainMethod} ({primaryKey}) in {duration:n0}ms",
				grainName,
				context.InterfaceMethod.Name,
				primaryKey,
				stopwatch.ElapsedMilliseconds
			);
			//}
		}
		catch (Exception ex)
		{
			_logger.LogError(
					ex,
					"Execution failed for grain method {grain}.{grainMethod} ({primaryKey}) in {duration:n0}ms.",
					primaryKey,
					grainName,
					context.InterfaceMethod.Name,
					stopwatch.ElapsedMilliseconds
				)
				;
			throw;
		}
	}
}