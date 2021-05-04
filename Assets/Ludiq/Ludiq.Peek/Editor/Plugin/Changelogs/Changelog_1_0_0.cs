using System;
using System.Collections.Generic;
using System.Linq;
using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(Changelog_1_0_0), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs
{
	// ReSharper disable once RedundantUsingDirective
	internal class Changelog_1_0_0 : PluginChangelog
	{
		public Changelog_1_0_0(global::Ludiq.PeekCore.Plugin plugin) : base(plugin) { }

		public override string description => "Initial Release. Welcome to Peek!";
		public override SemanticVersion version => "1.0.0";
		public override DateTime date => new DateTime(2019, 08, 08);
		public override IEnumerable<string> changes => Enumerable.Empty<string>();
	}
}