using System;
using Foundation;
using UIKit;

namespace SpirAtheneum.iOS.Platform
{
	public class MyActivityItemProvider : UIActivityItemProvider
	{
		public string SharedMessage { get; set; }
		public string TextContents { get; set; }
		public MyActivityItemProvider(NSObject a_placeholderItem) : base(a_placeholderItem)
		{
		}
		public override NSObject GetItemForActivity(UIActivityViewController activityViewController, NSString activityType)
		{
			base.GetItemForActivity(activityViewController, activityType);
			if (activityType == UIActivityType.PostToTwitter)
			{
				return new NSString(SharedMessage);
			}
			else
			{
				return new NSString(TextContents);
			}
		}
	}
}