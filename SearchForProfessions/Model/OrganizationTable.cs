//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SearchForProfessions.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrganizationTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationTable()
        {
            this.AdmissionPlanTable = new HashSet<AdmissionPlanTable>();
        }
    
        public int ID { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string E_mail { get; set; }
        public string Site { get; set; }
        public string Hotline { get; set; }
        public bool AvailableEnvironment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdmissionPlanTable> AdmissionPlanTable { get; set; }
    }
}
