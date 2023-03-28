using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchForProfessions.Model
{
    public partial class OrganizationTable
    {
        public string FullName
        {
            get
            {
                return Prefix + " " + Name;
            }
        }
    }
}
