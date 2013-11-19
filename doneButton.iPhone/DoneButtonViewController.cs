using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace doneButton.iPhone
{
	public class DoneButtonViewController : UIViewController
	{
		private UITextField _txtNumbers;
		private UITextField _txtSomethingElse;
		private UIButton _doneButton;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			Title = "Done Button Example";

			// Let's init our done button right away

			_doneButton = new UIButton (UIButtonType.Custom);
			_doneButton.Frame = new RectangleF (0, 163, 106, 53);
			_doneButton.SetTitle ("DONE", UIControlState.Normal);
			_doneButton.SetTitleColor (UIColor.Black, UIControlState.Normal);
			_doneButton.SetTitleColor (UIColor.White, UIControlState.Highlighted);
			_doneButton.TouchUpInside += (sender, e) => 
			{
				this.View.EndEditing(true);
			};

			var lbl = new UILabel (new RectangleF (5, 30, 200, 20));
			lbl.Text = "Enter some numbers";
			lbl.Font = UIFont.SystemFontOfSize (12f);
			View.AddSubview (lbl);

			_txtNumbers = new UITextField (new RectangleF (5, 50, 200, 20));
			_txtNumbers.KeyboardType = UIKeyboardType.NumberPad;
			_txtNumbers.BorderStyle = UITextBorderStyle.RoundedRect;
			View.AddSubview (_txtNumbers);

			var lbl2 = new UILabel (new RectangleF (5, 80, 200, 20));
			lbl2.Text = "Enter some text";
			lbl2.Font = UIFont.SystemFontOfSize (12f);
			View.AddSubview (lbl2);

			_txtSomethingElse = new UITextField (new RectangleF (5, 100, 200, 20));
			_txtSomethingElse.KeyboardType = UIKeyboardType.Default;
			_txtSomethingElse.BorderStyle = UITextBorderStyle.RoundedRect;
			_txtSomethingElse.EnablesReturnKeyAutomatically = true;
			_txtSomethingElse.ShouldReturn = ShouldReturn;
			View.AddSubview (_txtSomethingElse);

			NSNotificationCenter.DefaultCenter.AddObserver ("UIKeyboardWillShowNotification", KeyboardWillShow);
			NSNotificationCenter.DefaultCenter.AddObserver ("UIKeyboardWillHideNotification", KeyboardWillHide);
		}

		public bool ShouldReturn (UITextField textField)
		{
			this.View.EndEditing (true);
			return true;
		}

		public void KeyboardWillHide(NSNotification notification)
		{
			Console.WriteLine ("Keyboard will hide");

			var shared = UIApplication.SharedApplication;
			_doneButton.Hidden = true;
		}

		public void KeyboardWillShow(NSNotification notification)
		{
			Console.WriteLine ("Keyboard will show");
			var shared = UIApplication.SharedApplication;

			var keyboard = _txtNumbers.WeakInputDelegate as UIView;
			if (keyboard != null)
			{
				_doneButton.Hidden = false;
				keyboard.AddSubview (_doneButton);
			}
		}
	}
}

