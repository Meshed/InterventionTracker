
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
	[Activity (Label = "SessionActivity")]			
	public class SessionActivity : Activity
	{
		private string _sessionMethod;
		private int _sessionDuration;
		private int _childID;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Session);

			_sessionMethod = Intent.GetStringExtra ("sessionMethod");
			_sessionDuration = Intent.GetIntExtra ("sessionDuration", 0);
			_childID = Intent.GetIntExtra ("childID", 0);
		}
	}
}

