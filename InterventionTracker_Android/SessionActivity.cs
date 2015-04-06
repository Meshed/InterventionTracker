
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace InterventionTracker_Android
{
	[Activity (Label = "Intervention Tracker - Session", Icon = "@drawable/icon")]			
	public class SessionActivity : Activity
	{
		private string _sessionMethod;
		private int _sessionDuration;
		private int _childID;
		private int _numberOfInterventions = 0;
		TextView _timer = null;
		Button _interventionButton = null;
		Button _completeButton = null;
		private Timer _sessionTimer;
		private int _timeRemaining = 0;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Session);

			// Get passed values
			_sessionMethod = Intent.GetStringExtra ("sessionMethod");
			_sessionDuration = int.Parse (Intent.GetStringExtra ("sessionDuration"));
			_childID = Intent.GetIntExtra ("childID", 0);

			_timeRemaining = _sessionDuration * 60;

			// Get controls
			_timer = FindViewById<TextView>(Resource.Id.sessionTimer);
			_interventionButton = FindViewById<Button> (Resource.Id.interveneButton);
			_completeButton = FindViewById<Button> (Resource.Id.sessionCompleteButton);
			_timer.Text = TimeRemainingFormated ();

			// Event Handlers
			_interventionButton.Click += InterventionButton_Click;
			_completeButton.Click += CompleteButton_Click;
			_completeButton.Enabled = false;
			_completeButton.Visibility = ViewStates.Gone;

			_sessionTimer = new Timer ();
			_sessionTimer.Interval = 1000;
			_sessionTimer.Elapsed += _sessionTimer_Elapsed;
			_sessionTimer.Enabled = true;
		}

		void _sessionTimer_Elapsed (object sender, ElapsedEventArgs e)
		{
			_timeRemaining -= 1;
			RunOnUiThread (() => {
				_timer.Text = TimeRemainingFormated();
			});

			if (_timeRemaining <= 0) {
				_sessionTimer.Enabled = false;

				RunOnUiThread (() => {
					_completeButton.Visibility = ViewStates.Visible;
					_completeButton.Enabled = true;
					_interventionButton.Enabled = false;
				});
			}
		}

		void InterventionButton_Click (object sender, EventArgs e)
		{
			_numberOfInterventions += 1;
			_interventionButton.Text = _numberOfInterventions.ToString ();
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

		private string TimeRemainingFormated()
		{
			int minutes = 0;
			int seconds = 0;
			string formattedSeconds;

			minutes = _timeRemaining / 60;
			seconds = _timeRemaining % 60;

			formattedSeconds = (seconds < 10) ? ("0" + seconds.ToString ()) : seconds.ToString ();

			return string.Format ("{0}:{1}", minutes, formattedSeconds);
		}
	}
}

