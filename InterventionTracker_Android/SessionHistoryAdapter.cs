using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;

namespace InterventionTracker_Android
{
	public class SessionHistoryAdapter : BaseAdapter<Session>
	{
		Activity context;
		List<Session> sessionList;
		
		public SessionHistoryAdapter (Activity context, List<Session> sessionList)
		{
			this.context = context;
			this.sessionList = sessionList;
		}

		public override long GetItemId (int position)
		{
			return sessionList [position].Id;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get view to inflate
			var view = context.LayoutInflater.Inflate(Resource.Layout.SessionHistoryList, parent, false);

			// Get controls on the view
			var sessionDate = view.FindViewById<TextView> (Resource.Id.sessionDate);
			var numberOfRedirects = view.FindViewById<TextView> (Resource.Id.numberOfRedirects);
			var sessionDuration = view.FindViewById<TextView> (Resource.Id.sessionDuration);
			var sessionMethod = view.FindViewById<TextView> (Resource.Id.sessionMethod);

			// Populate the controls with data
			var session = sessionList[position];
			sessionDate.Text = DateTime.Parse (session.SessionDate).ToShortDateString ();
			numberOfRedirects.Text = session.NumberOfRedirects.ToString();
			sessionDuration.Text = session.SessionDuration.ToString();
			sessionMethod.Text = session.SessionMethod;

			return view;
		}

		public override int Count {
			get {
				return sessionList.Count;
			}
		}

		public override Session this [int index] {
			get {
				return sessionList [index];
			}
		}
	}
}

