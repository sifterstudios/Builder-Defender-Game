using System;
using System.Collections.Generic;
using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(Changelog_1_1_2), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs
{
	// ReSharper disable once RedundantUsingDirective
	internal class Changelog_1_1_2 : PluginChangelog
	{
		public Changelog_1_1_2(global::Ludiq.PeekCore.Plugin plugin) : base(plugin) { }
		
		public override SemanticVersion version => "1.1.2";
		public override DateTime date => new DateTime(2019, 09, 25);

		public override IEnumerable<string> changes
		{
			get
			{
				yield return "[Added] Tabs persistence when entering play mode";
				yield return "[Added] Editor preference for default editor popup width";
			}
		}
	}
}