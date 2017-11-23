using System;
using SQLite;

namespace SpirAtheneum.Models
{
    public class LastUpdateDailyDigest
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;

            }
        }
        private string _dailyDigestLastUpdateDate;
        private string _email;

        public string DailyDigestLastUpdateDate
        {
            get
            {
                return _dailyDigestLastUpdateDate;
            }
            set
            {
                this._dailyDigestLastUpdateDate = value;

            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                this._email = value;

            }
        }

    }
}
