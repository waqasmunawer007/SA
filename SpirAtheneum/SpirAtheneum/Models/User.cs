using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    class User
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

        private string _name;
        private string _fevoriteId;
        private string _mobileUserId;
        private double _subScriptionPrice;
        private bool isSubScribed;


        public string FevoriteId
        {
            get
            {
                return _fevoriteId;
            }
            set
            {
                this._fevoriteId = value;

            }
        }
        public string MobileUserId
        {
            get
            {
                return _mobileUserId;
            }
            set
            {
                this._mobileUserId = value;

            }
        }

        public bool IsSubscribed
        {
            get
            {
                return isSubScribed;
            }
            set
            {
                this.isSubScribed = value;

            }
        }

        public double SubScriptionPrice
        {
            get
            {
                return _subScriptionPrice;
            }
            set
            {
                this._subScriptionPrice = value;

            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;

            }
        }
        private string _email;
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

        private string _password;
        [MaxLength(50)]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                    this._password = value;
               
            }
        }
      
      

    }
}
