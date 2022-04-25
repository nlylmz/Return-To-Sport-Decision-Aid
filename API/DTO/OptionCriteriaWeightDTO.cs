using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{

    public class ValueDTO
    {
        public double Value { get; set; }
    }

    public class OptionCriteriaWeightDTO
    {
         public string CriteriaNames { get; set; }

        public List<double> Values { get; set; }
    }
}
