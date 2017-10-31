using System;
namespace SpirAtheneum.Constants
{
    public class AppConstant
    {
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
		#endregion
	}
}
