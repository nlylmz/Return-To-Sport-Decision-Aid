using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Criter
{
    public class CriteriaMap
    {
        public CriteriaMap(EntityTypeBuilder<Criteria> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Name);

            entityBuilder.HasMany(e => e.AthleteCriterias).WithOne(e => e.Criteria).HasForeignKey(e => e.CriteriaId);
        }
    }
}
