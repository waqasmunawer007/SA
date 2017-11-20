using System;
using Services.Models.Subscription;

namespace Services.Models.MobileUser
{
    public class AppMobileUser
    {
        public string id { get; set; }
        public string email { get; set; }
        public string pass_hash { get; set; }
        public string favorites_id { get; set; }
        public string subscription_type_id { get; set; }
        public string is_active { get; set; }
        public string created_at { get; set; }
        public Meta meta { get; set; }
    }
}
