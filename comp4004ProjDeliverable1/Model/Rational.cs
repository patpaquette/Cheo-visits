using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp4004ProjDeliverable1.Model
{
    class Rational
    {
        public int ID;
        public string Label;

        public Rational(int id, string label)
        {
            this.ID = id;
            this.Label = label;
        }

        public override string ToString()
        {
            return ID + ". " + Label;
        }
    }
}
