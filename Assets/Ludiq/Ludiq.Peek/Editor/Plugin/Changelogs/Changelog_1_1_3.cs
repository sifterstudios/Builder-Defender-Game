using System;
using System.Collections.Generic;
using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(Changelog_1_1_3), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs
{
	// ReSharper disable once RedundantUsingDirective
	internal class Changelog_1_1_3 : PluginChangelog
	{
		public Changelog_1_1_3(global::Ludiq.PeekCore.Plugin plugin) : base(plugin) { }
		
		public override SemanticVersion version => "1.1.3";
		public override DateTime date => new DateTime(2019, 09, 26);

		public override IEnumerable<string> changes
		{
			get
			{
				yield return "[Fixed] Custom window icons not working in tabs";
				yield return "[Fixed] Removed forgotten debug log statement when opening tabs";
			}
		}
	}
}