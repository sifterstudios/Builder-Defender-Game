using System;
using System.Collections.Generic;
using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs;
using Ludiq.PeekCore;

[assembly: MapToPlugin(typeof(Changelog_1_4_0), PeekPlugin.ID)]

namespace Ludiq.Ludiq.Peek.Editor.Plugin.Changelogs
{
	// ReSharper disable once RedundantUsingDirective
	internal class Changelog_1_4_0 : PluginChangelog
	{
		public Changelog_1_4_0(global::Ludiq.PeekCore.Plugin plugin) : base(plugin) { }

		public override SemanticVersion version => "1.4.0";

		public override DateTime date => new DateTime(2021, 04, 12);

		public override IEnumerable<string> changes
		{
			get
			{
				yield return "[Added] Dialog to Reparent Selection to specific Scene or Transform (Ctrl/Cmd+M)";
			}
		}
	}
}