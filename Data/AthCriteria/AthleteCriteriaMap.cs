using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AthCriteria
{
    public class AthleteCriteriaMap
    {
        public AthleteCriteriaMap(EntityTypeBuilder<AthleteCriteria> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.CriteriaId);
            entityBuilder.Property(e => e.AthleteId);
            entityBuilder.Property(e => e.Weight);

            entityBuilder.HasOne(e => e.Criteria).WithMany(e => e.AthleteCriterias).HasForeignKey(e => e.CriteriaId);
            entityBuilder.HasOne(e => e.Patient).WithMany(e => e.AthleteCriterias).HasForeignKey(e => e.AthleteId);
        }
    }
}
