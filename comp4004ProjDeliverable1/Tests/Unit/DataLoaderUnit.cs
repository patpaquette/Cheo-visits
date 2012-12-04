using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace comp4004ProjDeliverable1.Tests.Unit
{
    using Model;

    class DataLoaderUnit
    {
        [Fact]
        public void GetVisitDataTest()
        {
            string path = "Data/VisitDataTest.txt";

            Dictionary<int, List<Visit>> data = DataLoader.GetVisitData(path);

            Assert.True(data.Count == 6);
            Assert.True(data[1].Count == 2);
        }
    }
}
