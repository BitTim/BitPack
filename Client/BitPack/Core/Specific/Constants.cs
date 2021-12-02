using System;
using System.Collections.Generic;

namespace BitPack.Core.Specific
{
	static class Constants
	{
		public static readonly string AppName = "BitPack";
		public static readonly string Version = "v1.0.0";

		public static readonly string DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"/BitPack - Preview";
		public static readonly string UpdateFolder = DataFolder + @"/Update";

		public static readonly string SettingsPath = DataFolder + @"/settings.json";
		public static readonly string ManifestFile = @"/manifest.json";
		public static readonly string FileListFile = @"/fileList.txt";

		public static Dictionary<string, string> ThemeURIs = new Dictionary<string, string>
		{
			["Light"] = "Theme/AppTheme/LightTheme.xaml",
			["Dark"] = "Theme/AppTheme/DarkTheme.xaml"
		};

		public static Dictionary<string, string> AccentURIs = new Dictionary<string, string>
		{
			["Blue"] = "Theme/AccentColors/Blue.xaml",
			["Teal"] = "Theme/AccentColors/Teal.xaml",
			["Green"] = "Theme/AccentColors/Green.xaml",
			["Yellow"] = "Theme/AccentColors/Yellow.xaml",
			["Orange"] = "Theme/AccentColors/Orange.xaml",
			["Red"] = "Theme/AccentColors/Red.xaml",
			["Purple"] = "Theme/AccentColors/Purple.xaml",
			["Mono"] = "Theme/AccentColors/Mono.xaml",
			["HotCold"] = "Theme/AccentColors/HotCold.xaml",
			["Cyberpunk"] = "Theme/AccentColors/Cyberpunk.xaml",
			["Lavender"] = "Theme/AccentColors/Lavender.xaml",
			["Aqua"] = "Theme/AccentColors/Aqua.xaml",
			["Nature"] = "Theme/AccentColors/Nature.xaml",
			["Emerald"] = "Theme/AccentColors/Emerald.xaml",
			["Chocolate"] = "Theme/AccentColors/Chocolate.xaml",
			["Cyberpunk2"] = "Theme/AccentColors/Cyberpunk2.xaml",
		};

		public static readonly string ReleasesURL = "https://api.github.com/repos/BitTim/BitPack/releases";
		public static readonly string BaseDownloadURL = "https://github.com/BitTim/BitPack/releases/download";



		public static readonly List<string> Channels = new List<string>()
		{
			"stable",
			"experimental"
		};
	}
}
