using System;
using CoreAnimation;
using CoreGraphics;
using SpirAtheneum.iOS.CustomRenderer;
using SpirAtheneum.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginEntry), typeof(LoginEntryRenderer))]

namespace SpirAtheneum.iOS.CustomRenderer
{
    public class LoginEntryRenderer:EntryRenderer
    {
		private CALayer _line;
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> args)
		{
			base.OnElementChanged(args);
			if (this.Control == null) return;


            _line = null;
			UITextField textField = (UITextField)Control;

			// Most commonly customized for font and no border
			//textField.Font = UIFont.FromName(DXStyle.FontFamily, (float)DXStyle.FontSmall.Size);
			Control.BorderStyle = UITextBorderStyle.None;

            _line = new CALayer
            {
                BorderColor = UIColor.FromRGB(126, 49, 59).CGColor,
               
				BackgroundColor = UIColor.FromRGB(126, 49, 59).CGColor,
                //Frame = new CGRect(0, 5, Frame.Width * 2, 1f)
                Frame = new CGRect(0, (Frame.Height / 2), Frame.Width * 1.06, 5f)
			};

			Control.Layer.AddSublayer(_line);
			//// Use 'Done' on keyboard
			//textField.ReturnKeyType = UIReturnKeyType.Done;
			//textField.EnablesReturnKeyAutomatically = true;

			//// No auto-correct
			//textField.AutocorrectionType = UITextAutocorrectionType.No;
			//textField.SpellCheckingType = UITextSpellCheckingType.No;
			//textField.AutocapitalizationType = UITextAutocapitalizationType.Words;

			//// Misc.
			//textField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
			//textField.ClearsOnBeginEditing = false;
			//textField.ClearsOnInsertion = false;
			//textField.AdjustsFontSizeToFitWidth = false;
			//textField.KeyboardAppearance = UIKeyboardAppearance.Default;
		}
    }
}
