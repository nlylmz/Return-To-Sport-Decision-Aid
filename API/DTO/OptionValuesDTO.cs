using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class OptionValuesDTO
    {
        public long CriteriaId { get; set; }
        public long Option1Id { get; set; }
        public long Option2Id { get; set; }
        public long Value { get; set; }
    }
}
