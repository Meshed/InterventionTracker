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

	public class ChildList
	{
		public List<Child> Children { get; set; }

		public ChildList()
		{
			Children = new List<Child>();
		}

		public void Initialize()
		{
			for (int i = 0; i < 30; i++) {
				var child = new Child ();

				child.FullName = "Kenneth Brown";

				Children.Add (child);
			}
		}
	}
}

