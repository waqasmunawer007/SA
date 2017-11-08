using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Constants
{
    class APIsConstant
    {
        public static string BaseUrl = "http://api-sa.azurewebsites.net/";


        #region API's                                        
        public static string AllMeditation = "api/Meditation";  
        public static string DigestAPi = "api/DailyDigest";             
        public static string AllKnowledgeBase = "api/Knowledge";
        public static string AppActivity = "api/AppActivity";
        public static string Signup = "api/MobileUser";
        public static string UpdateFavourites = "api/Favorite";
        #endregion
        

        #region Fields Constants
        public const string Email = "email";
        public const string Password = "password";
        public const string Meditation_id = "id";
        public const string User_Id = "mobile_user_id";
        public const string ListOfMeditations = "meditations";
        public const string ListOfKnowledge = "knowledge";
        #endregion

        #region Error Messages
        public const string InvalidEmail = "Please enter a valid email !";
        public const string EmptyEmailAndPassword = "Email and Password cannot be empty !";
        public const string SignupNullResponse = "Something went wrong !";
        public const string SignupNullResponseTitle = "Oops";
        public const string OK = "OK";
        #endregion

    }
}
