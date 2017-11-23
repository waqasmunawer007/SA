using System;
using SQLite;

namespace SpirAtheneum.Models
{
    public class LastUpdateMeditation
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

        private string _meditationLastUpdateDate;
        private string _email;


        public string MeditationLastUpdateDate
        {
            get
            {
                return _meditationLastUpdateDate;
            }
            set
            {
                this._meditationLastUpdateDate = value;

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
