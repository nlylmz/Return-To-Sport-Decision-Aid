using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Feedback
{
    public class Feedbacks : BaseEntity
    {
        public string Evaluation { get; set; }
        public bool DoesAthlete { get; set; }
    }
}
