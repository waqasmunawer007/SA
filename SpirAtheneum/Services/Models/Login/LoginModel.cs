using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Login
{
    public class LoginModel : BaseResponse
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
