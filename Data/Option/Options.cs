using Data.AthleteOption;
using ReturnToSport.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Option
{
    public class Options : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<OptionResult> OptionResults { get; set; }
    }
}
