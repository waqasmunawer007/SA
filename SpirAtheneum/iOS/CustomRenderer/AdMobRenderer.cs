using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using SpirAtheneum.Renderers;
using SpirAtheneum.iOS.CustomRenderer;
using Xamarin.Forms.Platform.iOS;
using Google.MobileAds;
using CoreGraphics;
using SpirAtheneum.Constants;

[assembly: ExportRenderer(typeof(AdMob), typeof(AdMobRenderer))]
namespace SpirAtheneum.iOS.CustomRenderer
{
    public class AdMobRenderer : ViewRenderer
    {
        //ca-app-pub-4606547028021587~2308254400 app id
        //const string AdmobID = "ca-app-pub-4606547028021587/5519250418"; //mine
       //const string AdmobID = "ca-app-pub-5129849535433603/2834914773"; //default sample ad
       
        BannerView adView;
        bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                UIViewController viewCtrl = null;

                foreach (UIWindow v in UIApplication.SharedApplication.Windows)
                {
                    if (v.RootViewController != null)
                    {
                        viewCtrl = v.RootViewController;
                    }
                }
                adView = new BannerView(size: AdSizeCons.Banner, origin: new CGPoint(-10, 0))
                {
                    AdUnitID = AppConstant.AdmobUnitIdForIOS,
                    RootViewController = viewCtrl
                };

                adView.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen) this.AddSubview(adView);
                    viewOnScreen = true;
                };

                adView.LoadRequest(Request.GetDefaultRequest());
                base.SetNativeControl(adView);
            }
        }
    
    }
}