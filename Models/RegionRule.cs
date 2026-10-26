using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandData.Models
{
    public class RegionRule
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; } = null!;
        public RuleCategory Category { get; set; }

        public string? ValueText { get; set; }
        public decimal? ValueNumber { get; set; }
        public string? Comment { get; set; }
        public DateTime? EditionDate { get; set; }
        public string? Note { get; set; }

        public DateTime UpdatedAt { get; set; }
        public List<DocumentLink> Links { get; set; } = new();
    }
}
