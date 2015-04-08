
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

namespace InterventionTracker_Android
{
	[Activity (Label = "New Session", Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class NewSessionActivity : Activity
	{
		EditText sessionDurationText;
		EditText sessionMethodText;
		Button startButton;
		int _childID = 0;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.NewSession);

			// Get passed in values
			_childID = Intent.GetIntExtra("childID", 0);

			// Get the controls
			sessionDurationText = FindViewById<EditText>(Resource.Id.sessionDuration);
			sessionMethodText = FindViewById<EditText> (Resource.Id.sessionMethodEdit);
			startButton = FindViewById<Button> (Resource.Id.sessionStart);

			startButton.Click += StartButtonClicked;
		}

		private void StartButtonClicked(object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(SessionActivity));
			intent.PutExtra ("sessionDuration", sessionDurationText.Text);
			intent.PutExtra ("sessionMethod", sessionMethodText.Text);
			intent.PutExtra ("childID", _childID);
			StartActivity (intent);
		}

		protected override void OnStop ()
		{
			base.OnStop ();

			Finish ();
		}
	}
}

