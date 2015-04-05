
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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ChildDetail);

			var data = new SessionList ();
			data.Initialize ();

			var sessionList = FindViewById<ListView> (Resource.Id.sessionHistory);

			sessionList.Adapter = new SessionHistoryAdapter (this, data.Sessions);
		}
	}
}

