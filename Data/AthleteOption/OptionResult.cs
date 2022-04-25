using Data.Athlete;
using Data.Option;
using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AthleteOption
{
    public class OptionResult :BaseEntity
    {
        public long OptionId { get; set; }
        public long AthleteId { get; set; }
        public double Weight { get; set; }

        public virtual Options Options { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
