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
        public bool Something { get; set; }

        public Healing (int id, string name, string namePlural, int ammounttoheal, int price, bool something) : base(id, name, namePlural, price )
        {
            AmountToHeal = ammounttoheal;
            Something = something;
        }
    }
}
