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

[assembly: ExportRenderer(typeof(AdMob), typeof(AdMobRenderer))]
namespace SpirAtheneum.iOS.CustomRenderer
{
    public class AdMobRenderer : ViewRenderer
    {
        const string AdmobID = "ca-app-pub-3940256099942544/6300978111";

        BannerView adView;
        bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                adView = new BannerView(AdSizeCons.SmartBannerPortrait)
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