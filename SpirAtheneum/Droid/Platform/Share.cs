using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.PM;
using Android.Text;
using SpirAtheneum.Interfaces;
using SpirAtheneum.Platform;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Share))]
namespace SpirAtheneum.Platform
{
    public class Share : IShare
    {
        public Task Show(string TextContents, string ShareMessage)
        {
			// Resources resources = 

			Intent emailIntent = new Intent();
			emailIntent.SetAction(Intent.ActionSend);
			// Native email client doesn't currently support HTML, but it doesn't hurt to try in case they fix it
			emailIntent.PutExtra(Intent.ExtraText, Html.FromHtml(TextContents));
			emailIntent.PutExtra(Intent.ExtraSubject, "");
			emailIntent.SetType("message/rfc822");

			Intent sendIntent = new Intent(Intent.ActionSend);
			sendIntent.SetType("text/plain");

			Intent openInChooser = Intent.CreateChooser(emailIntent, "Choose Action");

			var resInfo = Forms.Context.PackageManager.QueryIntentActivities(sendIntent, 0);
			List<LabeledIntent> intentList = new List<LabeledIntent>();
			for (int i = 0; i < resInfo.Count; i++)
			{
				// Extract the label, append it, and repackage it in a LabeledIntent
				ResolveInfo ri = resInfo[i];
				String packageName = ri.ActivityInfo.PackageName;
				if (packageName.Contains("android.email"))
				{
					emailIntent.SetPackage(packageName);
				}
				else if (packageName.Contains("twitter") || packageName.Contains("facebook") || packageName.Contains("mms") || packageName.Contains("android.gm"))
				{
					Intent intent = new Intent();
					intent.SetComponent(new ComponentName(packageName, ri.ActivityInfo.Name));
					intent.SetAction(Intent.ActionSend);
					intent.SetType("text/plain");
					if (packageName.Contains("twitter"))
					{
                        intent.PutExtra(Intent.ExtraText, ShareMessage);
					}
					else if (packageName.Contains("facebook"))
					{
						// Warning: Facebook IGNORES our text. They say "These fields are intended for users to express themselves. Pre-filling these fields erodes the authenticity of the user voice."
						// One workaround is to use the Facebook SDK to post, but that doesn't allow the user to choose how they want to share. We can also make a custom landing page, and the link
						// will show the <meta content ="..."> text from that page with our link in Facebook.
                        intent.PutExtra(Intent.ExtraText, TextContents);
					}
					else if (packageName.Contains("mms"))
					{
						intent.PutExtra(Intent.ExtraText, TextContents);
					}
					else if (packageName.Contains("android.gm"))
					{ // If Gmail shows up twice, try removing this else-if clause and the reference to "android.gm" above
						intent.PutExtra(Intent.ExtraText, Html.FromHtml(TextContents));
						intent.PutExtra(Intent.ExtraSubject, "");
						intent.SetType("message/rfc822");
					}

					intentList.Add(new LabeledIntent(intent, packageName, ri.LoadLabel(Forms.Context.PackageManager), ri.Icon));
				}
			}

			// convert intentList to array
			LabeledIntent[] extraIntents = intentList.ToArray();
			openInChooser.PutExtra(Intent.ExtraInitialIntents, extraIntents);
			Forms.Context.StartActivity(openInChooser);
            return null;

		}
    }
}
