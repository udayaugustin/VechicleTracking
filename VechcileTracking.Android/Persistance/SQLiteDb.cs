using System;
using System.IO;
using SQLite;
using VechcileTracking.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace VechcileTracking.Droid
{
	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteAsyncConnection GetConnection()
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "MySQLite.db3");

			return new SQLiteAsyncConnection(path);
		}
	}
}

