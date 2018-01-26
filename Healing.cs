using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Healing : _items
    {
        public int AmountToHeal { get; set; }

        public Healing (int id, string name, string namePlural, int ammounttoheal, int price) : base(id, name, namePlural, price )
        {
            AmountToHeal = ammounttoheal;
        }
    }
}
