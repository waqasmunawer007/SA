using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SpirAtheneum.Interfaces;
using SQLite;
using System.IO;
using SpirAtheneum.iOS.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]
namespace SpirAtheneum.iOS.DependencyService
{
    class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "SpirAtheneumDB.db3";
            string personalFolder =
              System.Environment.
              GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder =
              Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}