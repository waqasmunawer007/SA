using System;
namespace Services.Models.Subscription
{
    public class AppSubscription
    {
		public string id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public double cost { get; set; }
		public string subscription_type { get; set; }
		public Meta meta { get; set; }
    }

	public class Meta
	{
		public string author { get; set; }
		public string date_added { get; set; }
        public string last_edited { get; set; }
	}
}
