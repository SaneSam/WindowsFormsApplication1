using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monster :LivingCreature
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxDmg { get; set; }
        public int RewardEXP { get; set; }
        public int Gold { get; set; }
        public int Sight { get; set; }
        public int Ac { get; set; }
        public string Weakness { get; set; }
        public List<LootItem> LootTable { get; set; }
        internal List<Inventory> lootItems { get; }
        
        public Monster (int id,string name, int maxdmg, int rewardexp, int gold,int hp,int maxhp,int stamina,int AC,int sight,string weakness) : base(hp,maxhp,stamina)
        {
            ID = id;
            Name = name;
            MaxDmg = maxdmg;
            RewardEXP = rewardexp;
            Gold = gold;
            Ac = AC;
            Sight = sight;
            LootTable = new List<LootItem>();
           lootItems= new List<Inventory>();
        }
        internal Monster NewInstanceOfMonster()
        {
            
            Monster NewMonster = new Monster(ID, Name, MaxDmg, RewardEXP, Gold, HP, MaxHP, Stamina, Ac, Sight, Weakness);
            foreach (LootItem lootItem in LootTable.Where(LootItem => RandomNumberGen.NumberBetween(1,100) <=LootItem.DropPercentage))
            {
                NewMonster.lootItems.Add(new Inventory(lootItem.Details, 1));
            }
            if(NewMonster.lootItems.Count == 0)
            {
                foreach(LootItem lootItem in LootTable.Where(x => x.IsDefaultItem))
                {
                    NewMonster.lootItems.Add(new Inventory(lootItem.Details, 1));
                }
            }
            return NewMonster;
        }
    }
}
