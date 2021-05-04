using System;
using Ludiq.PeekCore;

namespace Ludiq.Ludiq.PeekCore.Runtime.Profiling
{
	public struct ProfilingScope : IDisposable
	{
		public ProfilingScope(string name)
		{
			ProfilingUtility.BeginSample(name);
		}

		public void Dispose()
		{
			ProfilingUtility.EndSample();
		}
	}
}