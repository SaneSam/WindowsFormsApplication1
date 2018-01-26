using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class QuestCompletedItem
    {
        public _items Details { get; set; }
        public int Quanity { get; set; }

        public QuestCompletedItem(_items details, int quantity)
        {
            Details = details;
            Quanity = quantity;
        }

    }
}
