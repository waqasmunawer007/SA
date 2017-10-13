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

[assembly: ExportRenderer(typeof(UpgradeYellowButton), typeof(UpgradeYellowButtonRenderer))]
namespace SpirAtheneum.Droid.CustomRenderer
{
    class UpgradeYellowButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackgroundResource(Resource.Drawable.UpgradeYellowButton);
            }
        }
    }
}