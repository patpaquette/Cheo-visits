using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp4004ProjDeliverable1
{
    static class Util
    {
        private static Random gen = new Random();

        public static DateTime getRandomDate(DateTime start, DateTime end)
        {
            int range = ((TimeSpan)(end - start)).Days;
            return start.AddDays(gen.Next(range));
        }

        public static int getFactorial(int n)
        {
            if (n == 1)
            {
                return 1;
            }

            return n * getFactorial(n - 1);
        }
    }
}
