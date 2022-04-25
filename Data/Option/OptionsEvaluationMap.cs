using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Option
{
    public class OptionsEvaluationMap
    {
        public OptionsEvaluationMap(EntityTypeBuilder<OptionsEvaluation> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.CriteriaId);
            entityBuilder.Property(e => e.AthleteId);
            entityBuilder.Property(e => e.Option1Id);
            entityBuilder.Property(e => e.Option2Id);
            entityBuilder.Property(e => e.Value);

            entityBuilder.HasOne(e => e.Patient).WithMany(e => e.OptionsEvaluations).HasForeignKey(e => e.AthleteId);
        }
    }
}
