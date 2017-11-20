using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Google.MobileAds;
using SpirAtheneum.Constants;
using UIKit;

namespace SpirAtheneum.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());
            MobileAds.Configure(AppConstant.AdmobAppId);
            return base.FinishedLaunching(app, options);
        }
    }
}
