using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Util;

namespace InterventionTracker_Android
{
	[Activity (Label = "New Child", Icon = "@drawable/icon")]
	public class NewChildActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Log.Debug ("OnCreate", "OnCreate");

			// Create your application here
			SetContentView(Resource.Layout.NewChild);

			var addChildButton = FindViewById<Button> (Resource.Id.addChild);
			addChildButton.Click += AddChildClicked;
		}

		void AddChildClicked(object sender, EventArgs e)
		{
			var childRepository = new ChildRepository ();

			var name = FindViewById<EditText> (Resource.Id.nameText);
			var dob = FindViewById<EditText> (Resource.Id.dobText);
			var unit = FindViewById<EditText> (Resource.Id.unitText);

			// Hide software keyboard when the button is pressed
			var inputMethodManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
			inputMethodManager.HideSoftInputFromWindow(unit.WindowToken, 0);

			var child = new Child ();

			child.FullName = name.Text;
			child.DateOfBirth = dob.Text;
			child.Unit = unit.Text;

			try
			{
				childRepository.AddChildAsync (child);
				Toast.MakeText(this, "Child Added Successfully", ToastLength.Long).Show();
				Finish ();
			}
			catch(Exception exception) {
				Toast.MakeText(this, "Error adding child: " + exception.Message, ToastLength.Long).Show();
			}
		}

		protected override void OnStop ()
		{
			Log.Debug ("OnStop", "OnStop");
			base.OnStop ();
		}

		protected override void OnStart ()
		{
			Log.Debug ("OnStart", "OnStart");
			base.OnStart ();
		}

		protected override void OnPause ()
		{
			Log.Debug ("OnPause", "OnPause");
			base.OnPause ();
		}

		protected override void OnResume ()
		{
			Log.Debug ("OnResume", "OnResume");
			base.OnResume ();
		}
	}
}

