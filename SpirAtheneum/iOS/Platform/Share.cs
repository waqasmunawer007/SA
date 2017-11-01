using System;
using System.Threading.Tasks;
using Foundation;
using SpirAtheneum.Interfaces;
using SpirAtheneum.iOS.Platform;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Share))]
namespace SpirAtheneum.iOS.Platform
{
	public class Share : IShare
	{

		public async Task Show(string TextContents, string ShareMessage)
		{
			MyActivityItemProvider itemsProvider = new MyActivityItemProvider(new NSString("blah blah blah"));
			itemsProvider.TextContents = TextContents;
			itemsProvider.SharedMessage = ShareMessage;

			var items = new NSObject[] { itemsProvider };
			var activityController = new UIActivityViewController(items, null);
			var vc = GetVisibleViewController();

			NSString[] excludedActivityTypes = null;

			if (excludedActivityTypes != null && excludedActivityTypes.Length > 0)
				activityController.ExcludedActivityTypes = excludedActivityTypes;

			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				if (activityController.PopoverPresentationController != null)
				{
					activityController.PopoverPresentationController.SourceView = vc.View;
				}
			}

			await vc.PresentViewControllerAsync(activityController, true);
		}
		UIViewController GetVisibleViewController()
		{
			var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

			if (rootController.PresentedViewController == null)
				return rootController;

			if (rootController.PresentedViewController is UINavigationController)
			{
				return ((UINavigationController)rootController.PresentedViewController).TopViewController;
			}

			if (rootController.PresentedViewController is UITabBarController)
			{
				return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
			}

			return rootController.PresentedViewController;
		}
	}
}
