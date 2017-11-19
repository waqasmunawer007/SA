using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models.MobileUser;

namespace Services.Services.MobileUser
{
    public interface IMobileUser
    {
        Task<AppMobileUser> CreateMobileUser(AppMobileUser mobileUser);
        Task<AppMobileUser> UpdateMobileUser(AppMobileUser mobileUser);
    }
}
