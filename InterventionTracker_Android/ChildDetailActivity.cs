
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
	[Activity (Label = "Child Details", Icon="@drawable/icon")]			
	public class ChildDetailActivity : Activity
	{
		private int _childID = 0;
		
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ChildDetail);

			// Get data
			ChildRepository childRepository = new ChildRepository();
			SessionRepository sessionRepository = new SessionRepository ();
			_childID = int.Parse (Intent.GetLongExtra ("childID", 0).ToString ());
			Child child = null;
			List<Session> sessions = null;

			if (_childID > 0) 
			{
				child = await childRepository.GetByIDAsync (_childID);
				sessions = await sessionRepository.GetAllForChild (_childID);
			}

			// Get activity views
			var childName = FindViewById<TextView> (Resource.Id.childNameText);
			var childDOB = FindViewById<TextView> (Resource.Id.childDOBText);
			var childUnit = FindViewById<TextView> (Resource.Id.childUnitText);
			var sessionList = FindViewById<ListView> (Resource.Id.sessionHistory);
			var newSessionButton = FindViewById<Button> (Resource.Id.newSession);

			newSessionButton.Click += NewSessionClicked;

			// Populate controls
			if (child != null) 
			{
				childName.Text = child.FullName;
				childDOB.Text = child.DateOfBirth;
				childUnit.Text = child.Unit;
			}

			// Fill session list
			sessionList.Adapter = new SessionHistoryAdapter (this, sessions);
		}

		private void NewSessionClicked(object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(NewSessionActivity));
			intent.PutExtra ("childID", _childID);
			StartActivity (intent);
		}
	}
}

