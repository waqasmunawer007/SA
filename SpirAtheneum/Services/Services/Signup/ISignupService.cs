using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Signup
{
    public interface ISignupService
    {
        Task<string> Signup(Dictionary<string, object> parameters); 
    }
}
