using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;

namespace InterventionTracker_Android
{
	public class ChildAdapter : BaseAdapter<Child>
	{
		Activity context;
		List<Child> children;
		
		public ChildAdapter (Activity context, List<Child> children)
		{
			this.context = context;
			this.children = children;
		}

		public override long GetItemId (int position)
		{
			return children[position].Id;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get view to infalte
			var view = context.LayoutInflater.Inflate (Resource.Layout.ChildList, parent, false);

			// Get controls on the view
			var name = view.FindViewById<TextView> (Resource.Id.name);

			// Polupate the controls with data
			name.Text = children [position].FullName;

			return view;
		}

		public override int Count {
			get 
			{
				return children.Count;
			}
		}

		public override Child this [int index] {
			get {
				return children [index];
			}
		}
	}
}

