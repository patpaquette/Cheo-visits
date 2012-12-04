//struct containing information about an Actual combination to match

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp4004ProjDeliverable1.Model
{
    public struct ACV
    {
        public List<Visit> Visits;

        public ACV(List<Visit> visits)
        {
            this.Visits = visits;
        }

        public void Add(Visit visit)
        {
            this.Visits.Add(visit);
        }

        public override string ToString()
        {
            string output = "";

            foreach (Visit v in this.Visits)
            {
                output += v.ToString() + System.Environment.NewLine;
            }

            return output;
        }
    }
}
