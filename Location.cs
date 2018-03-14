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
        public int MonsterAppearanceChance { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Vendortron9000 Vending { get; set; }
        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }
        public bool Trap { get; set; }


        public bool HasAmonster { get { return _monsterAtLocation.Count > 0; } }
        public bool hasAQuest { get { return QuestAvailableHere != null; } }
        public bool NoReqiredItemToEnter { get { return Itemrequiredtoenter == null; } }

        public Location(int id, string name, string description,bool trap, _items itemrequired = null,
            Quest questfind = null)
        {
            ID = id;
            Name = name;
            Description = description;
            Itemrequiredtoenter = itemrequired;
            QuestAvailableHere = questfind;
            Trap = trap;
        }

        public void AddMonster(int MonsterID, int percentageOfApperance)
        {
            if (_monsterAtLocation.ContainsKey(MonsterID))
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
            if (!HasAmonster)
            {
                return null;
            }

            // If this location only has a monster appear some of the times,
            // check if a monster should appear this time.
            if (MonsterAppearanceChance > 0)
            {
                // Gets a random number between 1 and 100.
                // If the number is less than, or equal to, the MonsterAppearanceChance, create a monster
                bool monsterAppeared = RandomNumberGen.NumberBetween(1, 100) <= MonsterAppearanceChance;

                // If no monster appeared, return null (no monster this time)
                if (!monsterAppeared)
                {
                    return null;
                }
            }

            int totalPercentages = _monsterAtLocation.Values.Sum();
            int randomNumber = RandomNumberGen.NumberBetween(1, totalPercentages);
            int runningTotal = 0;
            foreach (KeyValuePair<int, int> monsterKeyValuePair in _monsterAtLocation)
            {
                runningTotal += monsterKeyValuePair.Value;

                if (randomNumber <= runningTotal)
                {
                    return World.MonsterByID(monsterKeyValuePair.Key).NewInstanceOfMonster();
                }

            }
            return World.MonsterByID(_monsterAtLocation.Keys.Last()).NewInstanceOfMonster();
        }
    }
}