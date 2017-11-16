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

[assembly: ExportRenderer(typeof(AdMob), typeof(AdMobRenderer))]
namespace SpirAtheneum.iOS.CustomRenderer
{
    public class AdMobRenderer : ViewRenderer
    {
        const string AdmobID = "ca-app-pub-5129849535433603/2834914773";

        BannerView adView;
        bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                adView = new BannerView(size: AdSizeCons.Banner,origin: new CGPoint(-10, 0))
                {
                    AdUnitID = AdmobID,
                    RootViewController = UIApplication.SharedApplication.Windows[0].RootViewController
                };
                adView.AdReceived += (sender, args) => {

                    if (!viewOnScreen) this.AddSubview(adView);
                    viewOnScreen = true;
                };
                adView.LoadRequest(Request.GetDefaultRequest());
                base.SetNativeControl(adView);
            }
        }
    }
}