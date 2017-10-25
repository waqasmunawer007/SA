using Services.Models.DailyDigest;
using Services.Models.KnowledgeBase;
using Services.Models.Meditation;
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
            database.CreateTable<FavouriteKnowledgeBase>();
            database.CreateTable<FavouriteMeditation>();
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

        /// <summary>
        /// Add dailydigest data into the DailyDigest database table
        /// </summary>
        /// <param name="dailyDigestList"></param>
        public void AddDailyDigest(List<DailyDigestModel> dailyDigestList)
        {
            lock (collisionLock)
            {
                database.Query<DailyDigest>("Delete from DailyDigest");
                database.Query<DailyDigest>("DELETE FROM SQLITE_SEQUENCE WHERE name = 'DailyDigest'");

                foreach (var d in dailyDigestList)
                {
                    DailyDigest dailyDigest = new DailyDigest();

                    dailyDigest.id = d.id;
                    dailyDigest.text = d.text;
                    dailyDigest.publish_date = d.publish_date.ToString();

                    database.Insert(dailyDigest);
                }
            }
        }

        /// <summary>
        /// Gets all the items from the database's DailyDigest table
        /// </summary>
        /// <returns></returns>
        public List<DailyDigest> GetDailyDigest()
        {
            lock(collisionLock)
            {
                var dailyDigestList = database.Table<DailyDigest>().ToList();
                return dailyDigestList;
            }
        }

        #endregion

        #region KnowledgeBase

        /// <summary>
        /// Add KnowledgeBase data into the KnowledgeBase database 
        /// </summary>
        /// <param name="KnowledgeList"></param>
        public void AddKnowledgeBase(List<KnowledgeBaseModel> KnowledgeList)
        {
            lock (collisionLock)
            {
                database.Query<KnowledgeBase>("Delete from KnowledgeBase");
                database.Query<KnowledgeBase>("DELETE FROM SQLITE_SEQUENCE WHERE name = 'KnowledgeBase'");

                foreach (var k in KnowledgeList)
                {
                    KnowledgeBase knowledgeBase = new KnowledgeBase();

                    knowledgeBase.id = k.id;
                    knowledgeBase.title = k.title;
                    knowledgeBase.text = k.text;
                    knowledgeBase.category = k.category;

                    database.Insert(knowledgeBase);
                }

                foreach (var f in KnowledgeList)
                {
                    if (!(database.Table<FavouriteKnowledgeBase>().Any(x => x.id == f.id)))
                    {
                        FavouriteKnowledgeBase favourite = new FavouriteKnowledgeBase();
                        favourite.id = f.id;
                        favourite.is_favourite = "false";

                        database.Insert(favourite);
                    }
                }
            }
        }

        /// <summary>
        /// Gets all the items from the database's KnowledgeBase table
        /// </summary>
        /// <returns></returns>
        public List<KnowledgeBase> GetKnowledgeBase()
        {
            lock (collisionLock)
            {
                var knowledgeList = database.Table<KnowledgeBase>().ToList();
                return knowledgeList;
            }
        }

        #endregion

        #region Meditation

        /// <summary>
        /// Add Meditation data into the Meditation database 
        /// </summary>
        /// <param name="meditationList"></param>
        public void AddMeditation(List<MeditationModel> meditationList)
        {
            lock (collisionLock)
            {
                database.Query<Meditation>("Delete from Meditation");
                database.Query<Meditation>("DELETE FROM SQLITE_SEQUENCE WHERE name = 'Meditation'");

                database.Query<Step>("Delete from Step");
                database.Query<Step>("DELETE FROM SQLITE_SEQUENCE WHERE name = 'Step'");

                foreach (var m in meditationList)
                {
                    Meditation meditation = new Meditation();

                    meditation.id = m.id;
                    meditation.title = m.title;
                    meditation.intro = m.intro;
                    meditation.outro = m.outro;
                    meditation.category = m.category;

                    database.Insert(meditation);

                    foreach (var s in m.steps)
                    {
                        Step step = new Step();

                        step.step = s;
                        step.meditation_id = m.id;

                        database.Insert(step);
                    }
                }

                foreach (var f in meditationList)
                {
                    if(!(database.Table<FavouriteMeditation>().Any(x => x.id == f.id)))
                    {
                        FavouriteMeditation favourite = new FavouriteMeditation();
                        favourite.id = f.id;
                        favourite.is_favourite = "false";

                        database.Insert(favourite);
                    }
                }
            }
        }

        /// <summary>
        /// Gets all the items from the database's Meditation table
        /// </summary>
        /// <returns></returns>
        public List<Meditation> GetMeditation()
        {
            lock (collisionLock)
            {
                var meditationList = database.Table<Meditation>().ToList();
                return meditationList;
            }
        }

        public List<Step> GetMeditationSteps()
        {
            lock(collisionLock)
            {
                var steps = database.Table<Step>().ToList();
                return steps;
            }
        }
        #endregion

        #region FavouriteMeditation

        /// <summary>
        /// Update favouritmeditation table in database
        /// </summary>
        /// <param name="id"></param>
        public void UpdateFavouriteMeditation(string id)
        {
            lock (collisionLock)
            {
                var f = database.Table<FavouriteMeditation>().First(x => x.id == id);
                if (f.is_favourite == "true")
                {
                    f.is_favourite = "false";
                }
                else if (f.is_favourite == "false")
                {
                    f.is_favourite = "true";
                }
                database.Update(f);
            }
        }

        /// <summary>
        /// Get data from MeditationFavourite database table
        /// </summary>
        /// <returns></returns>
        public List<FavouriteMeditation> GetMeditationFavourite()
        {
            lock(collisionLock)
            {
                var favouriteMeditationList = database.Table<FavouriteMeditation>().ToList();
                return favouriteMeditationList;
            }
        }

        #endregion

        #region FavouriteKnowledge

        /// <summary>
        /// Update FavouriteKnowledgeBase table in the database
        /// </summary>
        /// <param name="id"></param>
        public void UpdateFavouriteKnowledgeBase(string id)
        {
            lock (collisionLock)
            {
                var f = database.Table<FavouriteKnowledgeBase>().First(x => x.id == id);
                if (f.is_favourite == "true")
                {
                    f.is_favourite = "false";
                }
                else if (f.is_favourite == "false")
                {
                    f.is_favourite = "true";
                }
                database.Update(f);
            }
        }

        /// <summary>
        /// Get data from database table KnowledgeBase
        /// </summary>
        /// <returns></returns>
        public List<FavouriteKnowledgeBase> GetKnowledgeBaseFavourite()
        {
            lock (collisionLock)
            {
                var favouriteKnowledgeBaseList = database.Table<FavouriteKnowledgeBase>().ToList();
                return favouriteKnowledgeBaseList;
            }
        }

        #endregion
    }
}








