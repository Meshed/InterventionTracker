using System;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace InterventionTracker_Android
{
	public class Session
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		public int ChildID { get; set; }
		public string SessionDate { get; set; }
		public int NumberOfRedirects { get; set; }
		public int SessionDuration { get; set; }
		public string SessionMethod { get; set; }
	}
}

