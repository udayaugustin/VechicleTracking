﻿using System;
using System.IO;
using SQLite;
using VechcileTracking.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace VechcileTracking.iOS
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

