namespace Liv.io.GraphCrawler.ControlService.Properties
{
	internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
	{

		private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized (new Settings ())));

		public static Settings Default {
			get {
				return defaultInstance;
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public string DataDirectory {
			get {
				return ((string)(this ["DataDirectory"]));
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public string EdgesFilename {
			get {
				return ((string)(this ["EdgesFilename"]));
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public string NodesFilename {
			get {
				return ((string)(this ["NodesFilename"]));
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public string ResourcesTableFilename {
			get {
				return ((string)(this ["ResourcesTableFilename"]));
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public string ResourcesFolder {
			get {
				return ((string)(this ["ResourcesFolder"]));
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public string ClientCodeFolder {
			get {
				return ((string)(this ["ClientCodeFolder"]));
			}
		}
	}
}