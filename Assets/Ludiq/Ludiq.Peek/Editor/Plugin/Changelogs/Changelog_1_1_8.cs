using System;
using System.Collections.Generic;
using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(Changelog_1_1_9), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs
{
	// ReSharper disable once RedundantUsingDirective
	internal class Changelog_1_1_9 : PluginChangelog
	{
		public Changelog_1_1_9(global::Ludiq.PeekCore.Plugin plugin) : base(plugin) { }
		
		public override SemanticVersion version => "1.1.9";
		public override DateTime date => new DateTime(2020, 09, 11);

		public override IEnumerable<string> changes
		{
			get
			{
				yield return "[Fixed] Compatibility with Unity 2020.2 Beta";
				yield return "[Fixed] Build errors related to Odin Serializer when using non-standard build pipelines";
				yield return "[Fixed] NullReferenceException in reference property drawer when using Peek with Odin";
				yield return "[Fixed] Removed obsolete 'Generate Custom Inspector' menu item";
			}
		}
	}
}