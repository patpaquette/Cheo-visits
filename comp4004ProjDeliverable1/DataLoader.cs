using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace comp4004ProjDeliverable1
{
    using Model;

    static class DataLoader
    {
        public static Dictionary<int, List<Visit>> GetVisitData(string path)
        {
            Dictionary<int, List<Visit>> data = new Dictionary<int, List<Visit>>();
            Random r = new Random();

            foreach (string row in File.ReadLines(path))
            {
                string[] split = row.Split(',');
              
                if (split.Length > 0)
                {
                    int id = int.Parse(split[0]);
                    Visit visit = new Visit(id, r.Next(1, 10), int.Parse(split[2]), DateTime.Parse(split[1]));

                    if (data.ContainsKey(id))
                    {
                        data[id].Add(visit);
                    }
                    else
                    {
                        data.Add(id, new List<Visit>(){visit});
                    }
                }
            }

            return data;
        }
    }
}
