
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
	[Activity (Label = "Session")]			
	public class SessionActivity : Activity
	{
		private string _sessionMethod;
		private int _sessionDuration;
		private int _childID;
		private int _numberOfInterventions = 0;
		TextView _timer = null;
		Button _interventionButton = null;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Session);

			// Get passed values
			_sessionMethod = Intent.GetStringExtra ("sessionMethod");
			_sessionDuration = Intent.GetIntExtra ("sessionDuration", 0);
			_childID = Intent.GetIntExtra ("childID", 0);

			// Get controls
			_timer = FindViewById<TextView>(Resource.Id.sessionTimer);
			_interventionButton = FindViewById<Button> (Resource.Id.interveneButton);
			var completeButton = FindViewById<Button> (Resource.Id.sessionCompleteButton);

			// Event Handlers
			_interventionButton.Click += InterventionButton_Click;
			completeButton.Click += CompleteButton_Click;
		}

		void InterventionButton_Click (object sender, EventArgs e)
		{
			_numberOfInterventions += 1;
			_interventionButton.Text = _numberOfInterventions.ToString();
		}

		void CompleteButton_Click (object sender, EventArgs e)
		{
			SessionRepository sessionRepository = new SessionRepository ();
			Session session = new Session ();

			session.ChildID = _childID;
			session.NumberOfRedirects = _numberOfInterventions;
			session.SessionDate = DateTime.Now.Date.ToString();
			session.SessionDuration = _sessionDuration;
			session.SessionMethod = _sessionMethod;

			sessionRepository.AddSessionAsync (session);
			Finish ();
		}
	}
}

