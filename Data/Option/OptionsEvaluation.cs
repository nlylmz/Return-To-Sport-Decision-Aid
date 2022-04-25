using Data.Athlete;
using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Option
{
    public class OptionsEvaluation: BaseEntity
    {
        public long AthleteId { get; set; }
        public long CriteriaId { get; set; }
        public long Option1Id { get; set; }
        public long Option2Id { get; set; }
        public long Value { get; set; }

        //public virtual Options Options { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
