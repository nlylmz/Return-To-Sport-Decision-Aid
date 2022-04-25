using Data.AthCriteria;
using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Criter
{
    public class Criteria:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<AthleteCriteria> AthleteCriterias { get; set; }
        //public virtual ICollection<CriteriaValue> CriteriaValues { get; set; }
    }
}
