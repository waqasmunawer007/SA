using System;
using SQLite;

namespace SpirAtheneum.Models
{
    public class LastUpdateKB
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
        private string _knowledgBaseLastUpdateDate;
        private string _email;

        public string KnowledgBaseLastUpdateDate
        {
            get
            {
                return _knowledgBaseLastUpdateDate;
            }
            set
            {
                this._knowledgBaseLastUpdateDate = value;

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
