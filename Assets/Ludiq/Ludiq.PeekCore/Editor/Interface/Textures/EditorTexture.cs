﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ludiq.PeekCore
{
	public class EditorTexture
	{
		private EditorTexture()
		{
			personal = new Dictionary<int, Texture2D>();
			professional = new Dictionary<int, Texture2D>();
		}

		private EditorTexture(Texture2D texture) : this()
		{
			Ensure.That(nameof(texture)).IsNotNull(texture);

			personal.Add(texture.width, texture);
		}

		private EditorTexture(Texture texture) : this((Texture2D)texture) { }

		public static EditorTexture Single(Texture2D texture)
		{
			return Single((Texture)texture);
		}

		public static EditorTexture Single(Texture texture)
		{
			if (texture == null)
			{
				return null;
			}

			lock (singleCache)
			{
				if (!singleCache.TryGetValue(texture, out var editorTexture))
				{
					editorTexture = new EditorTexture(texture);
					singleCache.Add(texture, editorTexture);
				}

				return editorTexture;
			}
		}

		private static readonly Dictionary<Texture, EditorTexture> singleCache = new Dictionary<Texture, EditorTexture>();



		#region Fetching

		private readonly Dictionary<int, Texture2D> personal;

		private readonly Dictionary<int, Texture2D> professional;

		public Texture2D this[int resolution]
		{
			get
			{
				resolution = (int)(resolution * EditorGUIUtility.pixelsPerPoint);

				if (EditorGUIUtility.isProSkin)
				{
					Texture2D proAtResolution;

					if (!professional.TryGetValue(resolution, out proAtResolution))
					{
						if (professional.Count > 0)
						{
							proAtResolution = GetHighestResolution(professional);
							professional.Add(resolution, proAtResolution);
						}
						else
						{
							Texture2D personalAtResolution;

							if (!personal.TryGetValue(resolution, out personalAtResolution))
							{
								personalAtResolution = GetHighestResolution(personal);
								personal.Add(resolution, personalAtResolution);
							}

							return personalAtResolution;
						}
					}

					return proAtResolution;
				}

				{
					Texture2D personalAtResolution;

					if (!personal.TryGetValue(resolution, out personalAtResolution))
					{
						personalAtResolution = GetHighestResolution(personal);
						personal.Add(resolution, personalAtResolution);
					}

					return personalAtResolution;
				}
			}
		}

		public Texture2D Single()
		{
			if (EditorGUIUtility.isProSkin)
			{
				if (professional.Count > 1)
				{
					throw new InvalidOperationException();
				}
				else if (professional.Count == 1)
				{
					return professional.Values.Single();
				}
			}

			if (personal.Count > 1)
			{
				throw new InvalidOperationException();
			}
			else if (personal.Count == 1)
			{
				return personal.Values.Single();
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		private Texture2D GetHighestResolution(Dictionary<int, Texture2D> dictionary)
		{
			return dictionary.OrderByDescending(kvp => kvp.Key).Select(kvp => kvp.Value).FirstOrDefault();
		}

		#endregion



		#region Loading

		public static readonly TextureResolution[] StandardIconResolutions =
		{
			IconSize.Small,
			IconSize.Medium,
			IconSize.Large
		};

		public static readonly TextureResolution[] UnitResolutions =
		{
			1,
			2,
			4
		};

		public static EditorTexture Load(IEnumerable<IResourceProvider> resourceProviders, string path, CreateTextureOptions options, bool required)
		{
			foreach (var resources in resourceProviders)
			{
				var texture = Load(resources, path, options, false);

				if (texture != null)
				{
					return texture;
				}
			}

			if (required)
			{
				var message = new StringBuilder();
				message.AppendLine("Missing editor texture: ");

				foreach (var resources in resourceProviders)
				{
					message.AppendLine($"{resources.GetType().HumanName()}: {resources.DebugPath(path)}");
				}

				Debug.LogWarning(message.ToString());
			}

			return null;
		}

		public static EditorTexture Load(IEnumerable<IResourceProvider> resourceProviders, string path, TextureResolution[] resolutions, CreateTextureOptions options, bool required)
		{
			foreach (var resources in resourceProviders)
			{
				var texture = Load(resources, path, resolutions, options, false);

				if (texture != null)
				{
					return texture;
				}
			}

			if (required)
			{
				var message = new StringBuilder();
				message.AppendLine("Missing editor texture: ");

				foreach (var resources in resourceProviders)
				{
					message.AppendLine($"{resources.GetType().HumanName()}: {resources.DebugPath(path)}");
				}

				Debug.LogWarning(message.ToString());
			}

			return null;
		}

		public static EditorTexture Load(IResourceProvider resources, string path, CreateTextureOptions options, bool required)
		{
			using (ProfilingUtility.SampleBlock("Load Editor Texture"))
			{
				Ensure.That(nameof(resources)).IsNotNull(resources);
				Ensure.That(nameof(path)).IsNotNull(path);

				var set = new EditorTexture();
				var name = Path.GetFileNameWithoutExtension(path).PartBefore('@');
				var extension = Path.GetExtension(path);
				var directory = Path.GetDirectoryName(path);

				var personalPath = Path.Combine(directory, $"{name}{extension}");
				var professionalPath = Path.Combine(directory, $"{name}@Pro{extension}");

				if (resources.FileExists(personalPath))
				{
					var personalTexture = resources.LoadTexture(personalPath, options);

					if (personalTexture != null)
					{
						set.personal.Add(personalTexture.width, personalTexture);
					}
				}

				if (resources.FileExists(professionalPath))
				{
					var professionalTexture = resources.LoadTexture(professionalPath, options);

					if (professionalTexture != null)
					{
						set.professional.Add(professionalTexture.width, professionalTexture);
					}
				}

				if (set.personal.Count == 0)
				{
					if (required)
					{
						Debug.LogWarning($"Missing editor texture: {name}\n{resources.DebugPath(path)}");
					}

					// Never return an empty set; the codebase assumes this guarantee

					return null;
				}

				return set;
			}
		}

		public static EditorTexture Load(IResourceProvider resources, string path, TextureResolution[] resolutions, CreateTextureOptions options, bool required)
		{
			using (ProfilingUtility.SampleBlock("Load Editor Texture"))
			{
				Ensure.That(nameof(resources)).IsNotNull(resources);
				Ensure.That(nameof(path)).IsNotNull(path);
				Ensure.That(nameof(resolutions)).HasItems(resolutions);

				var set = new EditorTexture();
				var name = Path.GetFileNameWithoutExtension(path).PartBefore('@');
				var extension = Path.GetExtension(path);
				var directory = Path.GetDirectoryName(path);

				// Try with explicit resolutions first
				foreach (var resolution in resolutions)
				{
					var width = resolution.width;
					// var height = resolution.height;

					var personalPath = Path.Combine(directory, $"{name}@{width}x{extension}");
					var professionalPath = Path.Combine(directory, $"{name}@{width}x_Pro{extension}");

					if (resources.FileExists(personalPath))
					{
						set.personal.Add(width, resources.LoadTexture(personalPath, options));
					}

					if (resources.FileExists(professionalPath))
					{
						set.professional.Add(width, resources.LoadTexture(professionalPath, options));
					}
				}

				if (set.personal.Count == 0)
				{
					if (required)
					{
						Debug.LogWarning($"Missing editor texture: {name}\n{resources.DebugPath(path)}");
					}

					// Never return an empty set; the codebase assumes this guarantee

					return null;
				}

				return set;
			}
		}

		#endregion
	}
}
