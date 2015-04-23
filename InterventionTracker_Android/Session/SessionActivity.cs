using System;
using System.Timers;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace InterventionTracker_Android
{
	[Activity (Label = "Session", Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class SessionActivity : Activity
	{
		string _sessionMethod;
		int _sessionDuration;
		int _childID;
		int _numberOfInterventions;
		TextView _timer;
		Button _interventionButton;
		Button _completeButton;
		Timer _sessionTimer;
		int _timeRemaining;
		
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

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
			var sessionRepository = new SessionRepository ();
			var session = new Session ();

			session.ChildID = _childID;
			session.NumberOfRedirects = _numberOfInterventions;
			session.SessionDate = DateTime.Now.Date.ToString();
			session.SessionDuration = _sessionDuration;
			session.SessionMethod = _sessionMethod;

			sessionRepository.AddSessionAsync (session);
			Toast.MakeText (this, "Session Info Saved", ToastLength.Long).Show ();
			Finish ();
		}

		string TimeRemainingFormated()
		{
			int minutes;
			int seconds;
			string formattedSeconds;

			minutes = _timeRemaining / 60;
			seconds = _timeRemaining % 60;

			formattedSeconds = (seconds < 10) ? ("0" + seconds) : seconds.ToString ();

			return string.Format ("{0}:{1}", minutes, formattedSeconds);
		}
	}
}

