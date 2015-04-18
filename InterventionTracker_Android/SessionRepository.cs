using System;
using SQLite.Net.Async;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Util;

namespace InterventionTracker_Android
{
	public class SessionRepository
	{
		SQLiteAsyncConnection dbConn;

		public SessionRepository ()
		{
			var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			dbPath = System.IO.Path.Combine(dbPath, "InterventionTracker.db3");
			var platform = new SQLiteConnectionWithLock (
				new SQLitePlatformAndroid (), 
				new SQLiteConnectionString (dbPath, storeDateTimeAsTicks: false));

			var connectionFunc = new Func<SQLiteConnectionWithLock> (() => platform);
			dbConn = new SQLiteAsyncConnection (connectionFunc);
			dbConn.CreateTableAsync<Session>();
		}

		public async void AddSessionAsync(Session session)
		{
			try
			{
				await dbConn.InsertAsync(session);
			}
			catch(Exception exception) 
			{
				Console.WriteLine (exception.Message);
			}
		}

		public async Task<List<Session>> GetAllForChild(int childID)
		{
			List<Session> sessionList = null;

			try 
			{
				sessionList = await dbConn.Table<Session> ().Where (i => i.ChildID == childID).ToListAsync ();
			}
			catch (Exception ex) {
				Log.Debug ("SessionRepository", "EXCEPTION!!!! - " + ex.Message);
			}
			return sessionList;
		}
	}
}

