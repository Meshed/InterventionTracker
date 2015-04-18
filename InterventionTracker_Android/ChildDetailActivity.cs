
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
using System.Threading.Tasks;
using Android.Util;

namespace InterventionTracker_Android
{
	[Activity (Label = "Child Details", Icon="@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class ChildDetailActivity : Activity
	{
		private int _childID = 0;
		List<Session> _sessions = null;
		ListView _sessionListView = null;
		
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ChildDetail);

			// Get data
			ChildRepository childRepository = new ChildRepository();
			_childID = int.Parse (Intent.GetLongExtra ("childID", 0).ToString ());
			Child child = null;


			var childName = FindViewById<TextView> (Resource.Id.childNameText);
			var childDOB = FindViewById<TextView> (Resource.Id.childDOBText);
			var childUnit = FindViewById<TextView> (Resource.Id.childUnitText);
			_sessionListView = FindViewById<ListView> (Resource.Id.sessionHistory);
			var newSessionButton = FindViewById<Button> (Resource.Id.newSession);

			if (_childID > 0) 
			{
				child = await childRepository.GetByIDAsync (_childID);
			}

			// Get activity views


			newSessionButton.Click += NewSessionClicked;

			// Populate controls
			if (child != null) 
			{
				childName.Text = child.FullName;
				childDOB.Text = child.DateOfBirth;
				childUnit.Text = child.Unit;
			}

			// Fill session list
		}

		private void NewSessionClicked(object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(NewSessionActivity));
			intent.PutExtra ("childID", _childID);
			StartActivity (intent);
		}

		private async Task PopulateSessions()
		{
			SessionRepository sessionRepository = new SessionRepository ();
			_sessions = await sessionRepository.GetAllForChild (_childID);

			if (_sessions != null) {
				_sessionListView.Adapter = new SessionHistoryAdapter (this, _sessions);
			}
		}

		protected async override void OnResume ()
		{
			base.OnResume ();
			await PopulateSessions ();
		}
	}
}

