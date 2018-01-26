using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int rewardEXP { get; set; }
        public int Gold { get; set; }
        public List<QuestCompletedItem> QuestCompletedItem { get; set; }
        public _items rewardItem { get; set; }

        public Quest (int id, string name, string description, int rewardexp, int gold)
        {
            ID = id;
            Name = name;
            Description = description;
            rewardEXP = rewardexp;
            Gold = gold;
            QuestCompletedItem = new List<QuestCompletedItem>();
        }

    }
}
