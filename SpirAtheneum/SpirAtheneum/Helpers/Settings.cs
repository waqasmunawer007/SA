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
        private const string EmailKey = "email";
        private const string PasswordKey = "password";
        private const string MobileUserIdKey = "mobile_user_id";
        private const string FevouriteIdKey = "fev_id";
        private const string SubscriptionPriceKey = "sub_price";


		private static readonly string SettingsDefault = string.Empty;
        private static readonly bool LoginDefault = false;

        private const string DailyDigestKey = "DailyDigest";
        private const string MeditationKey = "Meditation";
        private const string KnowledgeBaseKey = "KnowledgeBase";
        private const string SubscriptionKey = "SubscriptionKey";

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
        public static string MobileUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(MobileUserIdKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(MobileUserIdKey, value);
            }
        }
        public static double SubscriptionPrice
        {
            get
            {
                return AppSettings.GetValueOrDefault(SubscriptionPriceKey, 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SubscriptionPriceKey, value);
            }
        }
        public static string FevouriteId
        {
            get
            {
                return AppSettings.GetValueOrDefault(FevouriteIdKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(FevouriteIdKey, value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(PasswordKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(PasswordKey, value);
            }
        }
      
        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault(EmailKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(EmailKey, value);
            }
        }
		public static bool IsSubscriped
		{
			get
			{
                return AppSettings.GetValueOrDefault(SubscriptionKey, false);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SubscriptionKey, value);
			}
		}
        public static bool IsLogin
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
        public static string DailyDigest_LastUpdate 
        {
            get
            {
                return AppSettings.GetValueOrDefault(DailyDigestKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(DailyDigestKey, value);
            }
        }
        public static string Meditation_LastUpdate
        {
            get
            {
                return AppSettings.GetValueOrDefault(MeditationKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(MeditationKey, value);
            }
        }
        public static string KnowledgeBase_LastUpdate
        {
            get
            {
                return AppSettings.GetValueOrDefault(KnowledgeBaseKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue(KnowledgeBaseKey, value);
            }
        }
    }
}