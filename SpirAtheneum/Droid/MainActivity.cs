﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SpirAtheneum.Droid
{
    [Activity(Label = "SpirAtheneum.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static MainActivity instance;
       // public static Context mContext;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //mContext = this;
            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
        public static MainActivity ShareInstance ()
        {
            if (instance == null)
            {
                instance = new MainActivity();
            }
            return instance;
        }
    }
}
