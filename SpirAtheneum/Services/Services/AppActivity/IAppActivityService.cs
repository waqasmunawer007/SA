using Services.Models.AppActivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.AppActivity
{
    public interface IAppActivityService
    {
        Task<AppActivityModel[]> FetchAppActivityAsync();
    }
}
