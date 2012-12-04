using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace comp4004ProjDeliverable1.Tests.Unit
{
    using Model;

    class ControllerUnit
    {
        [Fact]
        public void GenerateIteration3PatientsTest()
        {
            Controller cont = new Controller();
            int visitCount = 0;

            cont.createIteration3Patients(100, 10, 4, new List<GenerationRule>()
            {
                new GenerationRule(100, 20)
            });

            List<Patient> patients = DbMethods.getInstance().getPatients();
            visitCount = DbMethods.getInstance().getVisitsCount();

            Assert.True(patients.Count == 100);
            Assert.True(visitCount == 2000);            
        }
    }
}
