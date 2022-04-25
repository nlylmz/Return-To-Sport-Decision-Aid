using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class AthleteCriteriaValuesDTO : BaseEntity
    {
        public long AthleteId { get; set; }
        public List<CriteriaValueDTO> CriteriaValues { get; set; }
    }
}
