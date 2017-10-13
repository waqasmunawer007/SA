using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Login
{
    public interface ILoginService
    {
        Task<string> SignIn(Dictionary<string, object> parameters);
    }
}
