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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			Title = "Done Button Example";

			var lbl = new UILabel (new RectangleF (5, 30, 200, 20));
			lbl.Text = "Enter some numbers";
			lbl.Font = UIFont.SystemFontOfSize (12f);
			View.AddSubview (lbl);

			_txtNumbers = new UITextField (new RectangleF (5, 50, 200, 20));
			_txtNumbers.KeyboardType = UIKeyboardType.NumberPad;
			_txtNumbers.BorderStyle = UITextBorderStyle.RoundedRect;

			View.AddSubview (_txtNumbers);


			NSNotificationCenter.DefaultCenter.AddObserver ("UIKeyboardWillShowNotification", KeyboardWillShow);
		}

		public void KeyboardWillShow(NSNotification notification)
		{
			var doneButton = new UIButton (UIButtonType.Custom);
			doneButton.Frame = new RectangleF (0, 163, 106, 53);
			doneButton.SetTitle ("DONE", UIControlState.Normal);
			doneButton.SetTitleColor (UIColor.Black, UIControlState.Normal);
			doneButton.SetTitleColor (UIColor.White, UIControlState.Highlighted);

			doneButton.TouchUpInside += (sender, e) => 
			{
				_txtNumbers.ResignFirstResponder();
			};

			var keyboard = _txtNumbers.WeakInputDelegate as UIView;
			if (keyboard != null)
			{
				keyboard.AddSubview (doneButton);
			}
		}
	}
}

