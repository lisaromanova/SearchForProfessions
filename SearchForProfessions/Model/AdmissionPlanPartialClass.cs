using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchForProfessions.Model
{
    public partial class AdmissionPlanTable
    {
        public string Qualifications
        {
            get
            {
                List<AdmissionPlanQualificationTable> list = AdmissionPlanQualificationTable.ToList();
                string str = "";
                foreach(AdmissionPlanQualificationTable tables in list)
                {
                    str += tables.QualificationTable.Name + ", ";
                }
                if (str != "")
                {
                    return str.Substring(0, str.Length - 2);
                }
                return str;
            }
        }

        public string EntranceTestString
        {
            get
            {
                if (EntranceTest)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                }
            }
        }
    }
}
