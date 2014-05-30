namespace Liv.io.Host.Properties
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
		public int ControlServicePort {
			get {
				return ((int)(this ["ControlServicePort"]));
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public int ControlServicePath {
			get {
				return ((int)(this ["ControlServicePath"]));
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		public int WebserverPort {
			get {
				return ((int)(this ["ProxyPort"]));
			}
		}
	}
}