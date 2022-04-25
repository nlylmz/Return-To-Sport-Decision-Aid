using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AthCriteria
{
    public class CriteriaValueMap
    {
        public CriteriaValueMap(EntityTypeBuilder<CriteriaValue> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Criteria1Id);
            entityBuilder.Property(e => e.Criteria2Id);
            entityBuilder.Property(e => e.AthleteId);
            entityBuilder.Property(e => e.Value);

            //entityBuilder.HasOne(e => e.Criteria).WithMany(e => e.CriteriaValues).HasForeignKey(e => e.Criteria1Id);
            //entityBuilder.HasOne(e => e.Criteria).WithMany(e => e.CriteriaValues).HasForeignKey(e => e.Criteria2Id);
            entityBuilder.HasOne(e => e.Patient).WithMany(e => e.CriteriaValues).HasForeignKey(e => e.AthleteId);
        }
    }
}
