using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LootItem
    {
        public _items Details { get; set; }
        public int DropPercentage { get; set; }
        public bool IsDefaultItem { get; set; }

        public LootItem (_items details, int dropPercentage, bool isdefaultitem)
        {
            Details = details;
            DropPercentage = dropPercentage;
            IsDefaultItem = isdefaultitem;
        }
    }
}
