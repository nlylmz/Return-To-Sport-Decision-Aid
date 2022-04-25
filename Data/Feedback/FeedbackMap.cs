using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Feedback
{
    public class FeedbackMap
    {
        public FeedbackMap(EntityTypeBuilder<Feedbacks> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Evaluation);
            entityBuilder.Property(e => e.DoesAthlete);
        }
    }
}
