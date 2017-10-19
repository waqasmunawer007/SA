using SpirAtheneum.Interfaces;
using SpirAtheneum.Models;
using SpirAtheneum.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpirAtheneum.Database
{
    class DatabaseHelper
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public DatabaseHelper()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
        }

        /// <summary>
        /// Creates Database if not exist
        /// </summary>
        public void CreateDatabase()
        {
            database.CreateTable<User>();
            database.CreateTable<DailyDigest>();
            database.CreateTable<KnowledgeBase>();
            database.CreateTable<Meditation>();
            database.CreateTable<Step>();
        }

        #region User

        /// <summary>
        /// Signup for new user
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool AddUser(User u)
        {
            bool ifRegistered = false;
            lock (collisionLock)
            {
                if (isUserExist(u) == false) //user not exist in db, create new user
                {
                    database.Insert(u);
                    ifRegistered = true;

                }
            }
            return ifRegistered;
        }

        /// <summary>
        /// Checks the credentials of user
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool GetUser(User u)
        {
            bool check;
            lock (collisionLock)
            {
                check = database.Table<User>().Any(user => user.Email == u.Email && user.Password == u.Password);
            }
            return check;
        }

        /// <summary>
        /// Checks that if user exists or not in the db
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private bool isUserExist(User u)
        {
            lock (collisionLock)
            {
                return database.Table<User>().Any(user => user.Email == u.Email);
            }
        }

        #endregion

        #region DailyDigest



        #endregion
    }
}








