using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace InterventionTracker_Android
{
	public class Child
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		public string FullName { get; set; }
		public string DateOfBirth { get; set; }
		public string Unit { get; set; }
	}
}

