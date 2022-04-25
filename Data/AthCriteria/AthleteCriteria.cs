using Data.Athlete;
using Data.Criter;
using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AthCriteria
{
    public class AthleteCriteria:BaseEntity
    {
        public long CriteriaId { get; set; }
        public long AthleteId { get; set; }
        public double Weight { get; set; }
        public virtual Criteria Criteria { get; set; }
        public virtual Patient Patient { get; set; }

    }
}
