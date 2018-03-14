using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Weapon : _items
    {
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
        public string DmgType { get; set; }

        public Weapon(int id, string name, string namePlural, int mindmg, int maxdmg, int price,string dmgtype): base(id,name,namePlural,price)
        {
            MinDmg = mindmg;
            MaxDmg = maxdmg;
            DmgType = dmgtype;
        }
    }
}
