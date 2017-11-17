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
        // Meditation Api's                                        
        public static string AllMeditation = "api/Meditation";  //GET
        #endregion
        #region  Digest Api's   
                                             
        public static string DigestAPi = "api/DailyDigest";             //GET

        #endregion

        #region
        public static string AllKnowledgeBase = "api/Knowledge";
        #endregion

        #region AppActivity

        public static string AppActivity = "api/AppActivity";

        #endregion

        public static string AppSubscription = "api/Subscription";

    }
}
