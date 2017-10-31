using Services.Models.DailyDigest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.DailyDigestService
{
    public interface IDailyDigestService
    {
       Task<DailyDigestModel[]> FetchAllDigestAsync();
       Task<DailyDigestModel> FetchDigestBaseOnIdAsync(string id);
    }
}
