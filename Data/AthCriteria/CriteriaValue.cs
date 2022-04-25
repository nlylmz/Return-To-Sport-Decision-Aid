using Data.Athlete;
using Data.Criter;
using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AthCriteria
{
    public class CriteriaValue: BaseEntity
    {
        public long AthleteId { get; set; }
        public long Criteria1Id { get; set; }
        public long Criteria2Id { get; set; }
        public long Value { get; set; }
        //public virtual Criteria Criteria { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
