using System;
using System.Threading.Tasks;
using Android.Content;
using SpirAtheneum.Droid.Platform;
using SpirAtheneum.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(SendEmail))]
namespace SpirAtheneum.Droid.Platform
{
	public class SendEmail : ISendEmail
	{
        
		public Task Email(string email, string subject)
		{
			var emailIntent = new Intent(Android.Content.Intent.ActionSend);

			
            emailIntent.PutExtra(Android.Content.Intent.ExtraEmail, new string[] { email});
			

			emailIntent.PutExtra(Android.Content.Intent.ExtraSubject, subject);
           // emailIntent.PutExtra(Android.Content.Intent.Ex, subject);


			emailIntent.SetType("message/rfc822");

           // MainActivity.mContext.StartActivity(emailIntent);
			
			return null;

		}
	}
}
