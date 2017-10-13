// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SpirAtheneum.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
        private const string LoginKey = "login"; 


		private static readonly string SettingsDefault = string.Empty;
        private static readonly int LoginDefault = 0;

        #endregion


        public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}
        public static int Login
        {
            get
            {
                return AppSettings.GetValueOrDefault(LoginKey, LoginDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LoginKey, value);
            }
        }

    }
}