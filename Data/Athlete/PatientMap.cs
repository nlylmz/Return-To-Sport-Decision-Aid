using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Athlete
{
    public class PatientMap
    {
        public PatientMap(EntityTypeBuilder<Patient> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.FirstName);
            entityBuilder.Property(e => e.LastName);
            entityBuilder.Property(e => e.Detail);

            entityBuilder.HasMany(e => e.AthleteCriterias).WithOne(e => e.Patient).HasForeignKey(e => e.AthleteId);
            entityBuilder.HasMany(e => e.OptionResults).WithOne(e => e.Patient).HasForeignKey(e => e.AthleteId);
            entityBuilder.HasMany(e => e.CriteriaValues).WithOne(e => e.Patient).HasForeignKey(e => e.AthleteId);
        }
    }
}
