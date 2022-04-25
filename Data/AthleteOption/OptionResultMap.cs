using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AthleteOption
{
    public class OptionResultMap
    {
        public OptionResultMap(EntityTypeBuilder<OptionResult> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.OptionId);
            entityBuilder.Property(e => e.AthleteId);
            entityBuilder.Property(e => e.Weight);

            entityBuilder.HasOne(e => e.Options).WithMany(e => e.OptionResults).HasForeignKey(e => e.OptionId);
            entityBuilder.HasOne(e => e.Patient).WithMany(e => e.OptionResults).HasForeignKey(e => e.AthleteId);
        }
    }
}
