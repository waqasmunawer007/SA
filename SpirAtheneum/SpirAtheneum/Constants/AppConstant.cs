using System;
namespace SpirAtheneum.Constants
{
    public class AppConstant
    {
        public static string AdminEmail = "sovereignsunempire@protonmail.com";
        public static string AppName = "Spiritual Atheneum";
        public static string AppAuther = "SA Mobile";

        public static string AdmobAppId = "ca-app-pub-4606547028021587~2308254400";
        public static string AdmobUnitIdForIOS = "ca-app-pub-3940256099942544/2934735716"; //google test ads id
        public static string AdmobUnitIdForAndroid = "ca-app-pub-3940256099942544/5224354917"; //google test ads id


		#region Sign Up & Sign In
		// Meditation Api's  
        public static string EmailPatteren = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
        public static string Done = "OK";
		public static string Congratulation = "Congratulation!";
        public static string Sorry = "Sorry!";
        public static string Error = "Error!";
		public static string RegistrarionSuccess = "Your account has been registered.";
		public static string RegistrarionError = "Email already exist.Please try again.";

        public static string LoginError = "Invalid email or password. Please try again.";
        public static string CancelSubscriptionAlert = "Do you really want to cancel your subscription?";
        public static string CancelSubscription = "Your subscription has been cancelled.";
        public static string SubscriptionSuccess = "You subscription has been completed.";
        public static string SubscriptionChangeSuccess = "You subscription has been changed.";
        public static string SubscriptionError = "You subscription request has been failed. Please try again.";
        public static string PasswordUnmatchedError = "Password is unmatched.Please try again.";
        public static string SuccessPasswordChange = "Your password has been changed.";
		#endregion
	}
}
