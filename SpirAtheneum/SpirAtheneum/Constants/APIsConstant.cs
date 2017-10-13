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
        public static string AllMeditation = "api/Meditation";             //GET
        public static string MeditationBaseOnId = "api/Meditation/{id}";  // GET
        public static string CreateMeditation = "api/Meditation";        //POST
        public static string UpdateMeditation = "api/Meditation";       //PUT
        public static string DeleteMeditation = "api/Meditation/{id}";  // DELETE


        #endregion


    }
}
