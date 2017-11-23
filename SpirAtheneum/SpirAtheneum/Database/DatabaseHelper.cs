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
using SpirAtheneum.Helpers;
using System.Diagnostics;

namespace SpirAtheneum.Database
{
    class DatabaseHelper
    {
        private SQLiteConnection database;
        private static DatabaseHelper databaseHelper;
        private static object collisionLock = new object();

        private DatabaseHelper()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
        }

        public static DatabaseHelper GetInstance()
        {
            if (databaseHelper == null)
            {
                databaseHelper = new DatabaseHelper();
            }
            return databaseHelper;
        }

        /// <summary>
        /// Creates Database if not exist
        /// </summary>
        public void CreateDatabase()
        {
            database.CreateTable<User>();
            database.CreateTable<LastUpdateKB>();
            database.CreateTable<LastUpdateMeditation>();
            database.CreateTable<LastUpdateDailyDigest>();
            database.CreateTable<DailyDigest>();
            database.CreateTable<KnowledgeBase>();
            database.CreateTable<Meditation>();
            database.CreateTable<Step>();
            database.CreateTable<FavouriteKnowledgeBase>();
            database.CreateTable<FavouriteMeditation>();
        }


        #region Last Update Data Handling

        public LastUpdateMeditation GetLastMeditationDate()
        {
            try
            {
                lock (collisionLock)
                {
                    var lastUpdateDate = database.Table<LastUpdateMeditation>().First(x => x.Email == Settings.Email);
                    return lastUpdateDate;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void SaveLastMeditationDate(string date)
        {
            try
            {
                lock (collisionLock)
                {
                    bool ifDateAlreayAvailable = database.Table<LastUpdateMeditation>().Any(d => d.Email == Settings.Email);
                    if (ifDateAlreayAvailable)
                    {
                        //update the date
                        var lastUpdateDate = database.Table<LastUpdateMeditation>().First(x => x.Email == Settings.Email);
                        lastUpdateDate.MeditationLastUpdateDate = date;
                        database.Update(lastUpdateDate);
                    }
                    else
                    {
                        //add new date
                        LastUpdateMeditation lastMeditationDate = new LastUpdateMeditation();
                        lastMeditationDate.Email = Settings.Email;
                        lastMeditationDate.MeditationLastUpdateDate = date;
                        database.Insert(lastMeditationDate);

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SaveLastMeditationDate " + ex.Message);
            }
        }

        public LastUpdateDailyDigest GetLastDailyDigestDate()
        {
            try
            {
                lock (collisionLock)
                {
                    var lastDailyDigestUpdateDate = database.Table<LastUpdateDailyDigest>().First(x => x.Email == Settings.Email);
                    return lastDailyDigestUpdateDate;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void SaveLastUpdateDailyDigestDate(string date)
        {
            try
            {
                lock (collisionLock)
                {
                    bool ifDateAlreayAvailable = database.Table<LastUpdateDailyDigest>().Any(d => d.Email == Settings.Email);
                    if (ifDateAlreayAvailable)
                    {
                        //update the date
                        var lastUpdateDate = database.Table<LastUpdateDailyDigest>().First(x => x.Email == Settings.Email);
                        lastUpdateDate.DailyDigestLastUpdateDate = date;
                        database.Update(lastUpdateDate);
                    }
                    else
                    {
                        //add new date
                        LastUpdateDailyDigest lastDailyDigestDate = new LastUpdateDailyDigest();
                        lastDailyDigestDate.Email = Settings.Email;
                        lastDailyDigestDate.DailyDigestLastUpdateDate = date;
                        database.Insert(lastDailyDigestDate);

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SaveLastUpdateDailyDigestDate " + ex.Message);
            }
        }

        public LastUpdateKB GetLastUpdateKBDate()
        {
            try
            {
                lock (collisionLock)
                {
                    var lastUpdateDate = database.Table<LastUpdateKB>().First(x => x.Email == Settings.Email);
                    return lastUpdateDate;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void SaveLastUpdateKBDate(string date)
        {
            try
            {
                lock (collisionLock)
                {
                    bool ifDateAlreayAvailable = database.Table<LastUpdateKB>().Any(d => d.Email == Settings.Email);
                    if (ifDateAlreayAvailable)
                    {
                        //update the date
                        var lastUpdateDate = database.Table<LastUpdateKB>().First(x => x.Email == Settings.Email);
                        lastUpdateDate.KnowledgBaseLastUpdateDate = date;
                        database.Update(lastUpdateDate);
                    }
                    else
                    {
                        //add new date
                        LastUpdateKB lastKBDate = new LastUpdateKB();
                        lastKBDate.Email = Settings.Email;
                        lastKBDate.KnowledgBaseLastUpdateDate = date;
                        database.Insert(lastKBDate);

                    }
                } 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SaveLastUpdateKBDate " + ex.Message); 
            }
        }

        #endregion


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

        public void UpdateUserSubscription(bool subscriptionStatus, double subscriptionPrice, string mobileUserId, string fevId)
        {
            lock (collisionLock)
            {
                var u = database.Table<User>().First(x => x.Email == Settings.Email);
                u.SubScriptionPrice = subscriptionPrice;
                u.IsSubscribed = subscriptionStatus;
                u.MobileUserId = mobileUserId;
                u.FevoriteId = fevId;
                database.Update(u);
            }
        }

        /// <summary>
        /// Checks the credentials of user
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public User GetUser(User u)
        {
            try
            {
                User user = database.Table<User>().First(x => x.Email == u.Email);
                return user;
            }catch(Exception)
            {
                return null;
            }
           
            //bool check;
            //lock (collisionLock)
            //{
            //    check = database.Table<User>().Any(user => user.Email == u.Email && user.Password == u.Password);
            //}
            //return check;
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

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="password">Password.</param>
        public void ChangePassword(string password)
        {
            lock (collisionLock)
            {
                var u = database.Table<User>().First(x => x.Email == Settings.Email);
                u.Password = password;
                database.Update(u);
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
                int status = database.Table<DailyDigest>().Delete(e => e.email == Settings.Email);

                foreach (var d in dailyDigestList)
                {
                    DailyDigest dailyDigest = new DailyDigest();

                    dailyDigest.id = d.id;
                    dailyDigest.text = d.text;
                    dailyDigest.email = Settings.Email;
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
                var dailyDigestList = database.Table<DailyDigest>().Where(x => x.email == Settings.Email).ToList();
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
               int status = database.Table<KnowledgeBase>().Delete(e => e.email == Settings.Email);
                //database.Query<KnowledgeBase>("Delete from KnowledgeBase");
                //database.Query<KnowledgeBase>("DELETE FROM SQLITE_SEQUENCE WHERE name = 'KnowledgeBase'");

                foreach (var k in KnowledgeList)
                {
                    KnowledgeBase knowledgeBase = new KnowledgeBase();

                    knowledgeBase.id = k.id;
                    knowledgeBase.title = k.title;
                    knowledgeBase.text = k.text;
                    knowledgeBase.email = Settings.Email;
                    knowledgeBase.share_message = k.share_message;
                    knowledgeBase.category = k.category;

                    database.Insert(knowledgeBase);
                }

                foreach (var f in KnowledgeList)
                {
                    if (!(database.Table<FavouriteKnowledgeBase>().Any(x => x.id == f.id && x.email == Settings.Email)))
                    {
                        FavouriteKnowledgeBase favourite = new FavouriteKnowledgeBase();
                        favourite.id = f.id;
                        favourite.is_favourite = "false";
                        favourite.email = Settings.Email;
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
                var knowledgeList = database.Table<KnowledgeBase>().Where(x => x.email == Settings.Email).ToList();
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
                int status = database.Table<Meditation>().Delete(e => e.email == Settings.Email);
               
                foreach (var m in meditationList)
                {
                    Meditation meditation = new Meditation();

                    meditation.id = m.id;
                    meditation.title = m.title;
                    meditation.html_string = m.html_string;
                    meditation.share_message = m.share_message;
                    meditation.email = Settings.Email;
                    meditation.category = m.category;

                    database.Insert(meditation);
                }

                foreach (var f in meditationList)
                {
                    if(!(database.Table<FavouriteMeditation>().Any(x => x.id == f.id && x.email == Settings.Email)))
                    {
                        FavouriteMeditation favourite = new FavouriteMeditation();
                        favourite.id = f.id;
                        favourite.is_favourite = "false";
                        favourite.email = Settings.Email;
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
                var meditationList = database.Table<Meditation>().Where(x=> x.email == Settings.Email).ToList();
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
            try
            {
                lock (collisionLock)
                {
                    var f = database.Table<FavouriteMeditation>().First(x => x.id == id && x.email == Settings.Email);
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
            catch(Exception ex)
            {
                Debug.WriteLine("UpdateFavouriteMeditation " + ex.Message);
            }
        }

        /// <summary>
        /// Get data from MeditationFavourite database table
        /// </summary>
        /// <returns></returns>
        public List<FavouriteMeditation> GetMeditationFavourite()
        {
            try
            {
                lock (collisionLock)
                {
                    var favouriteMeditationList = database.Table<FavouriteMeditation>().Where(x => x.email == Settings.Email).ToList();
                    return favouriteMeditationList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetMeditationFavourite " + ex.Message);
            }
            return null;
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
                var f = database.Table<FavouriteKnowledgeBase>().First(x => x.id == id && x.email == Settings.Email);
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
            try
            {
                lock (collisionLock)
                {
                    var favouriteKnowledgeBaseList = database.Table<FavouriteKnowledgeBase>().Where(x => x.email == Settings.Email).ToList();
                    return favouriteKnowledgeBaseList;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine("GetKnowledgeBaseFavourite " + ex.Message);
            }
            return null;
        }

        #endregion
    }
}








