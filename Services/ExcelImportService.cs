using ClosedXML.Excel;
using LandData.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandData.Services
{
    public class ExcelImportService
    {
        // Один регион встречается в разных файлах, поэтому храним их по коду:
        // нашли — используем, не нашли — создаём.
        private readonly Dictionary<string, Region> _regions = new();

        public List<Region> Regions => _regions.Values.OrderBy(r => r.Code).ToList();

        // Файл «Жилой дом КФХ на сельхозке»: данные с 3-й строки,
        // A — код, B — регион, C — можно/нельзя, D — комментарий, E — ссылка
        public void ImportFarmHouse(string path)
        {
            using var workbook = new XLWorkbook(path);
            var ws = workbook.Worksheet(1);
            int lastRow = ws.LastRowUsed()!.RowNumber();

            for (int row = 3; row <= lastRow; row++)
            {
                string code = NormalizeCode(ws.Cell(row, 1));
                if (code == "") continue;                       // пустая строка

                var region = GetOrCreateRegion(code, ws.Cell(row, 2).GetString());
                var rule = CreateRule(region, RuleCategory.FarmHouse, ws.Cell(row, 3));
                rule.Comment = Clean(ws.Cell(row, 4));
                AddLink(rule, ws.Cell(row, 5));
            }
        }
        public void ImportRedistribution(string path)
        {
            using var workbook = new XLWorkbook(path);
            var ws = workbook.Worksheet(1);
            int lastRow = ws.LastRowUsed()!.RowNumber();

            for (int row = 3; row <= lastRow; row++)
            {
                string code = NormalizeCode(ws.Cell(row, 1));
                if (code == "") continue;

                var region = GetOrCreateRegion(code, ws.Cell(row, 2).GetString());
                var rule = CreateRule(region, RuleCategory.Redistribution, ws.Cell(row, 3));
                rule.Comment = Clean(ws.Cell(row, 4));
                AddLink(rule, ws.Cell(row, 5));
            }

        }

        public void ImportBuyoutAfter3Years(string path)
        {
            using var workbook = new XLWorkbook(path);
            var ws = workbook.Worksheet(1);
            int lastRow = ws.LastRowUsed()!.RowNumber();

            for (int row = 3; row <= lastRow; row++)
            {
                string code = NormalizeCode(ws.Cell(row, 1));
                if (code == "") continue;

                var region = GetOrCreateRegion(code, ws.Cell(row, 2).GetString());

                var price = CreateRule(region, RuleCategory.BuyoutAfter3Years, ws.Cell(row, 3));
                AddLink(price, ws.Cell(row, 5));
                AddLink(price, ws.Cell(row, 6));

                var moratoriumCell = ws.Cell(row, 4);
                if (!moratoriumCell.IsEmpty())
                    CreateRule(region, RuleCategory.Moratorium, moratoriumCell);
            }

        }

        // ---------- вспомогательные методы ----------

        // "01", "1", "1.0" -> "01"
        private static string NormalizeCode(IXLCell cell)
        {
            if (cell.IsEmpty()) return "";
            string raw = cell.GetString().Trim().Replace(',', '.');
            return double.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out var n)
                ? ((int)n).ToString("00")
                : "";
        }

        private Region GetOrCreateRegion(string code, string name)
        {
            if (!_regions.TryGetValue(code, out var region))
            {
                region = new Region { Code = code, Name = name.Trim() };
                _regions[code] = region;
            }
            return region;
        }

        private static RegionRule CreateRule(Region region, RuleCategory category, IXLCell valueCell)
        {
            var rule = new RegionRule
            {
                Region = region,
                Category = category,
                UpdatedAt = DateTime.Now
            };

            if (!valueCell.IsEmpty())
            {
                if (valueCell.DataType == XLDataType.Number)
                {
                    rule.ValueNumber = (decimal)valueCell.GetDouble();
                    rule.ValueText = $"{rule.ValueNumber * 100:0.##}%";   // 0.15 -> "15%"
                }
                else
                {
                    rule.ValueText = valueCell.GetString().Trim();
                }
            }

            region.Rules.Add(rule);
            return rule;
        }

        private static string? Clean(IXLCell cell)
        {
            string s = cell.GetString().Trim();
            return s == "" ? null : s;
        }

        private static void AddLink(RegionRule rule, IXLCell cell)
        {
            string url = cell.GetString().Trim();
            if (url.StartsWith("http"))
                rule.Links.Add(new DocumentLink { Rule = rule, Url = url });
        }
    }
}