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

        public string CategoryName => Category switch
        {
            RuleCategory.Buyout3918 => "Выкуп (ст. 39.18)",
            RuleCategory.Buyout3920House => "Выкуп (ст. 39.20, жилой дом)",
            RuleCategory.Buyout3920Other => "Выкуп (ст. 39.20, иные случаи)",
            RuleCategory.Redistribution => "Перераспределение",
            RuleCategory.BuyoutAfter3Years => "Выкуп через 3 года аренды",
            RuleCategory.Moratorium => "Мораторий",
            RuleCategory.FarmHouse => "Жилой дом КФХ на сельхозке",
            _ => Category.ToString()
        };
    }
}
