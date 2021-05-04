using System;
using System.Collections.Generic;
using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(Changelog_1_3_1), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs
{
	// ReSharper disable once RedundantUsingDirective
	internal class Changelog_1_3_1 : PluginChangelog
	{
		public Changelog_1_3_1(global::Ludiq.PeekCore.Plugin plugin) : base(plugin) { }

		public override SemanticVersion version => "1.3.1";

		public override DateTime date => new DateTime(2020, 10, 21);

		public override IEnumerable<string> changes
		{
			get
			{
				yield return "[Added] Option to preserve scale in Replacer";
				yield return "[Added] Option to place new Creator objects at scene root";
				yield return "[Added] Option to place avoid placing new Creator objects within prefab instances";
				yield return "[Fixed] Creator placing new objects in the active scene instead of the targeted object scene";
				yield return "[Fixed] Replacer placing replaced objects in the active scene instead of the original scene";
				yield return "[Fixed] Replacer not preserving sibling index";
				yield return "[Fixed] Potential type name conflicts with types in the global namespace";
			}
		}
	}
}