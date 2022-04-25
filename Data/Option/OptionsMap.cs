using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Option
{
    public class OptionsMap
    {
        public OptionsMap(EntityTypeBuilder<Options> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Name);

            entityBuilder.HasMany(e => e.OptionResults).WithOne(e => e.Options).HasForeignKey(e => e.OptionId);
        }
    }
}
