using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class DoctorOptionsEvaluationDTO : BaseEntity
    {
        public long AthleteId { get; set; }
        public List<OptionValuesDTO> OptionValues { get; set; }
    }
}
