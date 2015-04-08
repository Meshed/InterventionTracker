using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Views.InputMethods;

namespace InterventionTracker_Android
{
	[Activity (Label = "Intervention Tracker - New Child", Icon = "@drawable/icon", ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class NewChildActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

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
			var status = FindViewById<TextView> (Resource.Id.childAddStatus);

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
				status.Text = "Child Added Successfully";
			}
			catch(Exception exception) {
				status.Text = "There was a problem adding the child";
			}
		}
	}
}

