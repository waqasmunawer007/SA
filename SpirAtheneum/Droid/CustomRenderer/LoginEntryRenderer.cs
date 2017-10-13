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
using Xamarin.Forms.Platform.Android;
using SpirAtheneum.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(LoginEntry), typeof(LoginEntryRenderer))]
namespace SpirAtheneum.Droid.CustomRenderer
{
    class LoginEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetHintTextColor(global::Android.Graphics.Color.Rgb(255, 255, 255));
                Control.SetBackgroundResource(Resource.Drawable.LoginEntryBottomBorder);
            }
        }
    }
}