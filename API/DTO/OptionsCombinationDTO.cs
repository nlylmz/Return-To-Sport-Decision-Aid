using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class OptionsCombinationDTO
    {
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public long Option1Id { get; set; }
        public long Option2Id { get; set; }
        public string Criteria { get; set; }
        public long CriteriaId { get; set; }
    }
}
