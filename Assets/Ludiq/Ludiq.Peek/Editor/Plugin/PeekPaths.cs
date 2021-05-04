using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(PeekPaths), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin
{
	// ReSharper disable once RedundantUsingDirective
	public sealed class PeekPaths : PluginPaths
	{
		private PeekPaths(PeekPlugin plugin) : base(plugin) { }
	}
}