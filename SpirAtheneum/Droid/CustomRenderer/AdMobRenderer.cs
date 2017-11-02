using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using SpirAtheneum.Renderers;
using SpirAtheneum.Droid.CustomRenderer;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Ads;

[assembly: ExportRenderer(typeof(AdMob), typeof(AdMobRenderer))]
namespace SpirAtheneum.Droid.CustomRenderer
{
    public class AdMobRenderer : ViewRenderer<AdMob, AdView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AdMob> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var ad = new AdView(Forms.Context);
                ad.AdSize = AdSize.Banner;
                ad.AdUnitId = "ca-app-pub-3940256099942544/6300978111";

                var requestbuilder = new AdRequest.Builder();
                ad.LoadAd(requestbuilder.Build());

                SetNativeControl(ad);
            }
        }
    }
}