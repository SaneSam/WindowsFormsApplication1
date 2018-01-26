using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location
    {
        private readonly SortedList<int, int> _monsterAtLocation = new SortedList<int, int>();
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public _items Itemrequiredtoenter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Vendortron9000 Vending { get; set; }
        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }

        public bool HasAmonster { get { return _monsterAtLocation.Count > 0;  } }
        public bool hasAQuest { get { return QuestAvailableHere != null; } }
        public bool NoReqiredItemToEnter { get { return Itemrequiredtoenter == null; } }

        public Location(int id, string name, string description,_items itemrequired = null,
            Quest questfind= null)
        {
            ID = id;
            Name = name;
            Description = description;
            Itemrequiredtoenter = itemrequired;
            QuestAvailableHere = questfind;
        }
        public void AddMonster(int MonsterID, int percentageOfApperance)
        {
            if(_monsterAtLocation.ContainsKey(MonsterID))
            {
                _monsterAtLocation[MonsterID] = percentageOfApperance;
            }
            else
            {
                _monsterAtLocation.Add(MonsterID, percentageOfApperance);
            }
        }

        public Monster NewInstanceOfMonsterLivingHere()
        {

            if(!HasAmonster)
            {
                return null;
            }
            int totalPercentages = _monsterAtLocation.Values.Sum();
            int percentmonsternospawn =  100-totalPercentages;
            int randomnumber = RandomNumberGen.NumberBetween(1, 100);
            if(randomnumber>percentmonsternospawn)
            {
                return null;
            }
            int randomNumber = RandomNumberGen.NumberBetween(1, totalPercentages);
            int runningTotal = 0;
            foreach(KeyValuePair<int,int> monsterKeyValuePair in _monsterAtLocation)
            {
                runningTotal += monsterKeyValuePair.Value;

                if(randomNumber <= runningTotal)
                {
                    return World.MonsterByID(monsterKeyValuePair.Key).NewInstanceOfMonster();
                }
                
            }
            return World.MonsterByID(_monsterAtLocation.Keys.Last()).NewInstanceOfMonster();
        }
    }
}
