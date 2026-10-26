using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandData.Models
{
    public enum RuleCategory
    {
        Buyout3918,          // выкуп по ст. 39.18, сразу в собственность
        Buyout3920House,     // выкуп по ст. 39.20 для собственника жилого дома
        Buyout3920Other,     // выкуп по ст. 39.20 в иных случаях
        Redistribution,      // перераспределение
        BuyoutAfter3Years,   // выкуп через 3 года аренды
        Moratorium,          // мораторий
        FarmHouse            // жилой дом КФХ на сельхозке
    }
}
