using Ludiq.Ludiq.Peek.Editor.Plugin;
using Ludiq.Peek;
using Ludiq.PeekCore;
using UnityEditor;
using UnityEngine;
using Tool = Ludiq.Peek.Tool;

namespace Ludiq.Ludiq.Peek.Editor.Tools.Utility
{
	// ReSharper disable once RedundantUsingDirective
	public sealed class InconsistentComponentsTool : Tool
	{
		public InconsistentComponentsTool()
		{
			icon = PeekPlugin.Icons.inconsistentComponents?[IconSize.Small];
			iconSize = icon.PointSize();
		}

		public override string tooltip => "More Hidden";

		private WarningWindow window;

		public override bool isActive => PopupWatcher.IsOpenOrJustClosed(window);

		public override void Open(ToolControl control)
		{
			PopupWatcher.Release(window);
			window = WarningWindow.Show(control.activatorScreenPosition);
			PopupWatcher.Watch(window);
		}

		private class WarningWindow : LudiqEditorWindow
		{
			public static WarningWindow Show(Rect activator)
			{
				var window = CreateInstance<WarningWindow>();
				window.ShowAsDropDown(activator, new Vector2(250, 34));
				return window;
			}

			protected override void OnGUI()
			{
				base.OnGUI();

				GUILayout.BeginHorizontal();
				GUILayout.Space(-3);
				GUILayout.BeginVertical();
				GUILayout.Space(-3);
				EditorGUILayout.HelpBox("Components that are only on some of the selected objects cannot be multi-edited.", MessageType.Info);
				GUILayout.Space(-3);
				GUILayout.EndVertical();
				GUILayout.Space(-5);
				GUILayout.EndHorizontal();
				
				if (e.type == EventType.Repaint)
				{
					LudiqGUI.DrawEmptyRect(new Rect(Vector2.zero, position.size), ColorPalette.unityBackgroundDark);
				}
			}
		}
	}
}