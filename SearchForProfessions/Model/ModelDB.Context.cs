﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BaseOfSpecializationsEntities : DbContext
    {
        public BaseOfSpecializationsEntities()
            : base("name=BaseOfSpecializationsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdmissionPlanQualificationTable> AdmissionPlanQualificationTable { get; set; }
        public virtual DbSet<AdmissionPlanTable> AdmissionPlanTable { get; set; }
        public virtual DbSet<EducationLevelTable> EducationLevelTable { get; set; }
        public virtual DbSet<FinancialBasisTable> FinancialBasisTable { get; set; }
        public virtual DbSet<FormOfTrainingTable> FormOfTrainingTable { get; set; }
        public virtual DbSet<OrganizationTable> OrganizationTable { get; set; }
        public virtual DbSet<QualificationTable> QualificationTable { get; set; }
        public virtual DbSet<SpecializationQualificationTable> SpecializationQualificationTable { get; set; }
        public virtual DbSet<SpecializationTable> SpecializationTable { get; set; }
    }
}
