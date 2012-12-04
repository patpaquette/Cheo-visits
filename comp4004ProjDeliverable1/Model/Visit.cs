using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp4004ProjDeliverable1.Model
{
    public class Visit
    {
        private int _ID;
        private Patient _patient;
        private int _professionalID;
        private int _rationalID;
        private DateTime _date;
        private Rational _rational;

        public Visit(Patient p, int ID, int professionalID, int rationalID, DateTime date) : this(ID, professionalID, rationalID, date)
        {
            this._patient = p;
            
        }

        public Visit(int ID, int professionalID, int rationalID, DateTime date)
        {
            this._ID = ID;
            this._professionalID = professionalID;
            this._rationalID = rationalID;
            this._date = date;
            this._rational = new Rational(rationalID, DbMethods.getInstance().getRational(rationalID));
        }

        public int ID
        {
            get
            {
                return this._ID;
            }

            set
            {
                this._ID = value;
            }
        }

        public Patient Patient
        {
            get
            {
                return this._patient;
            }

            set
            {
                this._patient = value;
            }
        }

        public int ProfessionalID
        {
            get
            {
                return this._professionalID;
            }
        }

        public int RationalID
        {
            get
            {
                return this._rationalID;
            }
        }

        public DateTime Date
        {
            get
            {
                return this._date;
            }
        }

        public string ShortString
        {
            get
            {
                return String.Format("{0:yyyy-MM-dd}", this.Date) + ", " + this._rational.Label;
            }
        }

        //checkes if this visit is equal to a specified visit
        public bool equals(Visit v)
        {
            if (!(v.Date.Date == this._date.Date))
            {
                return false;
            }

            if (!(v.RationalID == this._rationalID))
            {
                return false;
            }

            return true;
        }

        //checks if this visit matches the cm
        public bool matches(CM cm)
        {
            if (!(cm.Date == this._date.Date))
            {
                return false;
            }

            if (!(cm.Rational == this._rationalID))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return  "ID : " + this._ID + " " + System.Environment.NewLine +
                    "Date : " + this.Date.Date.ToString() + " " + System.Environment.NewLine +
                    "Rational : " + this._rationalID;
        }

    }
}
