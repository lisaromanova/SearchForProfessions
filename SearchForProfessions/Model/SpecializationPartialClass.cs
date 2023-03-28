using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchForProfessions.Model
{
    public partial class SpecializationTable
    {
        public string FullName
        {
            get => Code + " " + Name;
        }

        public string Qualifications
        {
            get
            {
                List<SpecializationQualificationTable> qualifications = SpecializationQualificationTable.ToList();
                string str = "";
                foreach(SpecializationQualificationTable tables in qualifications)
                {
                    str += tables.QualificationTable.Name + "\n";
                }
                return str;
            }
        }
    }
}
