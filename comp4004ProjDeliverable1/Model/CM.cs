using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp4004ProjDeliverable1.Model
{
    public struct CM
    {
        public DateTime Date;
        public int Rational;

        public CM(DateTime date, int rational)
        {
            this.Date = date;
            this.Rational = rational;
        }
    }
}
