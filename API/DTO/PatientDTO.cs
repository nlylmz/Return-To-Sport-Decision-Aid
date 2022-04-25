using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class PatientDTO :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Detail { get; set; }
    }
}
