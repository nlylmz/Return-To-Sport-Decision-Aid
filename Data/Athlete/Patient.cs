using Data.AthCriteria;
using Data.AthleteOption;
using Data.Option;
using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Athlete
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Detail { get; set; }
        public virtual ICollection<AthleteCriteria> AthleteCriterias { get; set; }
        public virtual ICollection<OptionResult> OptionResults { get; set; }
        public virtual ICollection<CriteriaValue> CriteriaValues { get; set; }
        public virtual ICollection<OptionsEvaluation> OptionsEvaluations { get; set; }
    }
}
