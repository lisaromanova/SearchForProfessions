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
        public void CheckFieldsAdmissionPlan_SpacesAtTheBeginningAndAtTheEndInPeriod()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, " 3 г. 10 мес. ", "75", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsQualification_IsTrue()
        {
            bool actual = CheckFieldsClasses.CheckFieldsQualification("Программист");
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsQualification_SpacesInTheMiddleInName()
        {
            bool actual = CheckFieldsClasses.CheckFieldsQualification("Разработчик   веб и мультимедийных приложений");
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_IsTrue()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("09.02.07", "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID=1, Name="Программист"} });
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_IncorrectCode()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("123", "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_SpacesInTheMiddleAndAtTheEndInName()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("09.02.07", "Информационные системы   и программирование  ", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsFalse(actual);
        }
    }
}
