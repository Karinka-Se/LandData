using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandData.Models
{
    public class DocumentLink
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public RegionRule Rule { get; set; } = null!;
        public string Url { get; set; } = string.Empty;
    }
}
