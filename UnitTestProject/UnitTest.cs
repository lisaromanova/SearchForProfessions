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
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, "3 г. 10 мес.", true, false);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_TypeIsBool()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, "3 г. 10 мес.", true, false);
            Assert.IsInstanceOfType(actual, typeof(bool));
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_NotNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, "3 г. 10 мес.", true, false);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_SpacesAtTheBeginningAndAtTheEndInPeriod()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, " 3 г. 10 мес. ", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_OrganizationIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(-1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_SpecializationIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, -1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, 1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_ListQualificationIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable>(), 1, 1, 1, " 3 г. 10 мес. ", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_FormIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, -1, 1, 1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_FinanceIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, -1, 1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_LevelIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, -1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_PeriodIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, -1, string.Empty, true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_PlanIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, -1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_EntranceTestIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, -1, "3 г. 10 мес.", false, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_PlanIsDouble()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, -1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsAdmissionPlan_PlanIsString()
        {
            bool actual = CheckFieldsClasses.CheckFieldsAdmissionPlan(1, 1, new List<QualificationTable> { new QualificationTable { ID = 1, Name = "Программист" } }, 1, 1, -1, "3 г. 10 мес.", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsQualification_IsTrue()
        {
            bool actual = CheckFieldsClasses.CheckFieldsQualification("Программист");
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsQualification_TypeIsBoll()
        {
            bool actual = CheckFieldsClasses.CheckFieldsQualification("Программист");
            Assert.IsInstanceOfType(actual, typeof(bool));
        }

        [TestMethod]
        public void CheckFieldsQualification_NotNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsQualification("Программист");
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CheckFieldsQualification_SpacesInTheMiddleInName()
        {
            bool actual = CheckFieldsClasses.CheckFieldsQualification("Разработчик   веб и мультимедийных приложений");
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsQualification_EmptyName()
        {
            bool actual = CheckFieldsClasses.CheckFieldsQualification(string.Empty);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_IsTrue()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("09.02.07", "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID=1, Name="Программист"} });
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_TypeIsBool()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("09.02.07", "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsInstanceOfType(actual, typeof(bool));
        }

        [TestMethod]
        public void CheckFieldsSpecialization_NotNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("09.02.07", "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_IncorrectCode()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("123", "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_EmptyCode()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization(string.Empty, "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_EmptyName()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("09.02.07", string.Empty, new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_SpacesInTheMiddleAndAtTheEndInName()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("09.02.07", "Информационные системы   и программирование  ", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsSpecialization_SpacesAtTheStartAndAtTheEndInCode()
        {
            bool actual = CheckFieldsClasses.CheckFieldsSpecilization("  09.02.07  ", "Информационные системы и программирование", new List<QualificationTable>() { new QualificationTable() { ID = 1, Name = "Программист" } });
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_IsTrue()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_TypeIsBool()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsInstanceOfType(actual, typeof(bool));
        }

        [TestMethod]
        public void CheckFieldsOrganization_NotNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SpacesInTheMiddleInName()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский    колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SpacesInTheMiddleInPhone()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12    (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SpacesInTheMiddleInAdress()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе     1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SpacesInTheMiddleInEmail()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_  suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SpacesInTheMiddleInSite()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngk  nn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SpacesInTheMiddleInBpoo()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12    (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SpacesAtTheStartAndAtTheEndInPrefix()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("  ГБПОУ  ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_PreficIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization(string.Empty, "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_NameIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", string.Empty, "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_PhoneIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", string.Empty, "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_AdressIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", string.Empty, "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_EmailIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", string.Empty, "https://ngknn.ru/", "218-22-12 (доб.322, 323)", true, false);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_SiteIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", string.Empty, "218-22-12 (доб.322, 323)", true, false);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_BpooIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", string.Empty, true, false);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckFieldsOrganization_AvailableEnvironmentIsNull()
        {
            bool actual = CheckFieldsClasses.CheckFieldsOrganization("ГБПОУ", "Нижегородский Губернский колледж", "218 22 12 (доб 309)", "Московское шоссе 1", "ngk_suz@mail.52gov.ru", "https://ngknn.ru/", "218-22-12 (доб.322, 323)", false, false);
            Assert.IsFalse(actual);
        }
    }
}
