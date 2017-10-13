//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using SpirAtheneum.Droid.CustomRenderer;
//using Xamarin.Forms;
//using SpirAtheneum.Renderers;
//using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(AdMob), typeof(AdMobRenderer))]
//namespace SpirAtheneum.Droid.CustomRenderer
//{
//    class AdMobRenderer : ViewRenderer<AdMob, Android.Gms.Ads.AdView>
//    {
//        protected override void OnElementChanged(ElementChangedEventArgs<AdMob> e)
//        {
//            base.OnElementChanged(e);

//            if (Control == null)
//            {
//                var ad = new Android.Gms.Ads.AdView(Forms.Context);
//                ad.AdSize = Android.Gms.Ads.AdSize.Banner;
//                ad.AdUnitId = "_your_admob_ad_unit_id_goes_here_";

//                var requestbuilder = new Android.Gms.Ads.AdRequest.Builder();
//                ad.LoadAd(requestbuilder.Build());

//                SetNativeControl(ad);
//            }
//        }
//    }
//}