using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(PeekManifest), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin
{
	// ReSharper disable once RedundantUsingDirective
	public sealed class PeekManifest : PluginManifest
	{
		private PeekManifest(PeekPlugin plugin) : base(plugin) { }

		public override string name => "Peek";

		public override string author => "Ludiq";

		public override string description => "Workflow Tools for Unity.";

		public override SemanticVersion version => "1.4.0";
	}
}