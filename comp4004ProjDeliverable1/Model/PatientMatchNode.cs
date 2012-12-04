using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp4004ProjDeliverable1.Model
{
    class PatientMatchNode
    {
        private List<Patient> _matches;
        private Patient _patient;

        public Patient Patient
        {
            get
            {
                return this._patient;
            }
        }

        public List<Patient> Matches
        {
            get
            {
                return this._matches;
            }
        }

        public PatientMatchNode(Patient v)
        {
            this._matches = new List<Patient>();
            this._patient = v;
        }

        public void addMatch(Patient match)
        {
            _matches.Add(match);
        }

        public int getMatchesNumber()
        {
            return this._matches.Count;
        }
    }
}
