using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SearchForProfessions;
using System.Collections.Generic;
using SearchForProfessions.Model;
using SearchForProfessions.Classes;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CheckFieldsAdmissionPlan_IsTrue()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, "3 г. 10 мес.", "75", true, false);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_IsFalse()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, " 3 г. 10 мес. ", "75", true, false);
            Assert.IsFalse(actual);
        }
    }
}
