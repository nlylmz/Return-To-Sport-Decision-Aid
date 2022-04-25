using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CriteriaValueDTO :BaseEntity
    {
        public long Criteria1Id { get; set; }
        public long Criteria2Id { get; set; }
        public long Value { get; set; }
    }
}
