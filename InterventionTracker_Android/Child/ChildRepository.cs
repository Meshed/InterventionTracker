using System;
using System.Collections.Generic;
using SQLite.Net.Async;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System.Threading.Tasks;

namespace InterventionTracker_Android
{
	public class ChildRepository
	{
		SQLiteAsyncConnection dbConn;

		public ChildRepository ()
		{
			var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			dbPath = System.IO.Path.Combine(dbPath, "InterventionTracker.db3");
			var platform = new SQLiteConnectionWithLock (
				new SQLitePlatformAndroid (), 
				new SQLiteConnectionString (dbPath, storeDateTimeAsTicks: false));

			var connectionFunc = new Func<SQLiteConnectionWithLock> (() => platform);
			dbConn = new SQLiteAsyncConnection (connectionFunc);
			dbConn.CreateTableAsync<Child>();
		}

		public async void AddChildAsync(Child child)
		{
			try
			{
				await dbConn.InsertAsync(child);
			}
			catch(Exception exception)
			{
				Console.WriteLine (exception.Message);
			}
		}

		public async Task<List<Child>> GetAllAsync()
		{
			List<Child> childList;

			childList = await dbConn.Table<Child> ().ToListAsync ();

			return childList;
		}

		public async Task<Child> GetByIDAsync(int childID)
		{
			return await dbConn.Table<Child> ().Where (i => i.Id == childID).FirstOrDefaultAsync ();
		}
	}
}

