using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class FeedbacksDTO: BaseEntity
    {
        public string Evaluation { get; set; }
        public bool DoesAthlete { get; set; }
    }
}
