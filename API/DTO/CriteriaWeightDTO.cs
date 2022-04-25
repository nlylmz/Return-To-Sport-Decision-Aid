using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CriteriaWeightDTO: BaseEntity
    {
        public string CriteriaName { get; set; }
        public double Weight { get; set; }
    }
}
