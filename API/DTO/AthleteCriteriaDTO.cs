using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class AthleteCriteriaDTO :BaseEntity
    {
        public List<CriteriaDTO> Criteria{ get; set; }
        public long AthleteId { get; set; }

    }
}
