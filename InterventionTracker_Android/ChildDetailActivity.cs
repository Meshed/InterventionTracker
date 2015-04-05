
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
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ChildDetail);

			// Get data
			ChildRepository childRepository = new ChildRepository();
			SessionRepository sessionRepository = new SessionRepository ();
			int childID = int.Parse (Intent.GetLongExtra ("childID", 0).ToString ());
			Child child = null;
			List<Session> sessions;

			if (childID > 0) 
			{
				child = await childRepository.GetByIDAsync (childID);
				sessions = await sessionRepository.GetAllForChild (childID);
			}

			// Get activity views
			var childName = FindViewById<TextView> (Resource.Id.childNameText);
			var childDOB = FindViewById<TextView> (Resource.Id.childDOBText);
			var childUnit = FindViewById<TextView> (Resource.Id.childUnitText);
			var sessionList = FindViewById<ListView> (Resource.Id.sessionHistory);

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
	}
}

