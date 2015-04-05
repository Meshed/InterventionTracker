using System;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace InterventionTracker_Android
{
	public class Session
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		public string SessionDate { get; set; }
		public int NumberOfRedirects { get; set; }
		public int SessionDuration { get; set; }
		public string SessionMethod { get; set; }
	}

	public class SessionList
	{
		public List<Session> Sessions { get; set; }

		public SessionList()
		{
			Sessions = new List<Session> ();
		}

		public void Initialize()
		{
			for (int i = 0; i < 10; i++) {
				var session = new Session ();

				session.Id = i+1;
				session.SessionDate = "4/" + (i + 1) + "/2014";
				session.NumberOfRedirects = i + 1;
				session.SessionDuration = 1;
				session.SessionMethod = "Toasted purple dino poop";

				Sessions.Add (session);
			}
		}
	}
}

