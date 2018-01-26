using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreature 
    {
        private int _gold;
        private int _EXP;
        private int _strength;
        private Location _currentlocation;
        public event EventHandler<MessageEventArgs> OnMessage;
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged("Gold");
            }
        }
        public int EXP
        {
            get { return _EXP; }
         private set
            {
                _EXP = value;
                OnPropertyChanged("EXP");
                OnPropertyChanged("Level");
            }
        }
        public int level
        {
            get { return ((EXP / 100) + 1); }
        }
        public int Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
                OnPropertyChanged("Stats");
            }
        }
        public int Endurance { get; set; }
        public int Speed { get; set; }
        public int Sight { get; set; }
        public int Intelligence { get; set; }
        public int Charisma {get;set;}
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public Weapon CurrentWeapon { get; set; }
        private Monster CurrentMonster { get; set; }
        public BindingList<Inventory> Inventory { get; set; }
        public BindingList<PlayerQuest> Quest { get; set; }  
        public Location CurrentLocation
        {
            get {return _currentlocation; }
            set
            {
                _currentlocation = value;
                OnPropertyChanged("CurrentLocation");
            }
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private Player (int hp, int maxhp, int stamina, int gold, int exp, int strength, int endurance, int speed, int sight, int intelligence, int charisma, int ArmorClass) : base (hp, maxhp,stamina)
        {
            Stamina = stamina;
            Gold = gold;
            EXP = exp;
            Strength = strength;
            Endurance = endurance;
            Speed = speed;
            Sight = sight;
            Intelligence = intelligence;
            Charisma = charisma;
            Inventory = new BindingList<Inventory>();
            Quest = new BindingList<PlayerQuest>();
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public List<Weapon> Weapons
        {
            get { return Inventory.Where(x => x.Details is Weapon).Select(x => x.Details as Weapon).ToList(); }
        }
        public List<Healing> Potions
        {
            get { return Inventory.Where(x => x.Details is Healing).Select(x => x.Details as Healing).ToList(); }
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private void raiseInventoryChangedEvent(_items item)
        {
            if(item is Weapon)
            {
                OnPropertyChanged("Weapons");
            }
            if(item is Healing)
            {
                OnPropertyChanged("Potion");
            }
        }
        public void RemoveItemFromInventory(_items itemToRemove, int quantity = 1)
        {
            Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToRemove.ID);
            if(item != null)
            {
                item.Quantity -= quantity;
                if(item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }
                raiseInventoryChangedEvent(itemToRemove);
            }
        }
        public void additemtoinventor(_items itemtoadd, int quantity = 1)
        {
            Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemtoadd.ID);
            if(item == null)
            {
                Inventory.Add(new Inventory(itemtoadd, quantity));
            }
            else
            {
                item.Quantity+= quantity;
            }
            raiseInventoryChangedEvent(itemtoadd);
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public void UsePotion(Healing potion)
        {
            RaiseMessage("You drink a " + potion.Name);

            HealPlayer(potion.AmountToHeal);

            RemoveItemFromInventory(potion);
            if(CurrentLocation.HasAmonster)
            {
                LetTheMonsterAttack();
            }
            
        }
        public void UseWeapon(Weapon weapon)
        {
            RaiseMessage(CurrentMonster.Name);
            int damageToMonster = RandomNumberGen.NumberBetween(weapon.MinDmg, weapon.MaxDmg);
            int dice = RandomNumberGen.NumberBetween(1, 20) + (int)(Sight/10);
            if(dice == 20)
            {
                CurrentMonster.HP -= damageToMonster*2;
                RaiseMessage("You hit the " + CurrentMonster.Name + " for " + (damageToMonster*2) + ".");
            }
            else if(dice >= CurrentMonster.Ac)
            {
                CurrentMonster.HP -= damageToMonster + (int)(Strength/5);
                RaiseMessage("You hit the " + CurrentMonster.Name + " for " + damageToMonster + ".");
            }
            else
            {
                RaiseMessage("You missed the " + CurrentMonster.Name);
            }
            if(CurrentMonster.IsDead)
            {
                LootTheCurrentMonster();
                MoveTo(CurrentLocation);
            }
            else
            {
                LetTheMonsterAttack();
            }
            
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private void RaiseMessage (string message, bool addExtraNewLine = false)
        {
            if(OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(message, addExtraNewLine));
            }
            
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public void MoveTo(Location location)
        {
            if(Stamina<=0)
            {
                Stamina = 0;
            }
            if (!HasRequiredItemToEnter(location))
            {
                RaiseMessage("You must have a " + location.Itemrequiredtoenter.Name + " to enter this location.");
                return;
            }
            CurrentLocation = location;
            if(location.hasAQuest)
            {
                int quest = 1;
                if (PlayerDoesNotHaveThisQuest(location.QuestAvailableHere))
                {
                    GiveQuestToPlayer(World.QuestByID(quest));
                }  
                else
                {
                    if(PlayerHasNotCompleted(World.QuestByID(quest)) && PlayerHasAllQuestCompletionItemFor(World.QuestByID(quest)))
                    {
                        GivePlayerQuestRewards(World.QuestByID(quest));
                        quest++;
                        if(World.QuestByID(quest) != null)
                        {
                        GiveQuestToPlayer(World.QuestByID(quest));
                        }
                    }
                }
            }

                SetTheCurrentMonsterForTheCurrentLocation(location);

            
        }
        public void moveNorth()
        {
            if(CurrentLocation.HasAmonster)
            {
                LetTheMonsterAttack();
            }
            if (Stamina <= 0)
            {
                Stamina = 0;
                RaiseMessage("Take a rest you are tired of walking");
            }
            else if (CurrentLocation.LocationToNorth != null)
            {
                MoveTo(CurrentLocation.LocationToNorth);
            }
        }
        public void moveEast()
        {
            if (CurrentLocation.HasAmonster)
            {
                LetTheMonsterAttack();
            }
            if (Stamina <= 0)
            {
                Stamina = 0;
                RaiseMessage("Take a rest you are tired of walking");
            }
            else if (CurrentLocation.LocationToEast != null)
            {
                MoveTo(CurrentLocation.LocationToEast);
            }
        }
        public void moveSouth()
        {
            if (CurrentLocation.HasAmonster)
            {
                LetTheMonsterAttack();
            }
            if (Stamina <= 0)
            {
                Stamina = 0;
                RaiseMessage("Take a rest you are tired of walking");
            }
            else if (CurrentLocation.LocationToSouth != null)
            {
                MoveTo(CurrentLocation.LocationToSouth);
            }         
        }
        public void moveWest()
        {
            if (CurrentLocation.HasAmonster)
            {
                LetTheMonsterAttack();
            }
            if (Stamina <= 0)
            {
                Stamina = 0;
                RaiseMessage("Take a rest you are tired of walking");
            }
            else if (CurrentLocation.LocationToWest != null)
            {
                MoveTo(CurrentLocation.LocationToWest);
            }      
        }
        public void Rest()
        {
            if (World.LocationByID(World.Location_ID_Camp) == CurrentLocation)
            {
                HP = MaxHP;
                Stamina = 100;
            }
            else
            {
                if (MaxHP <= (HP+(int)(MaxHP * .15)))
                {
                    HP = MaxHP;
                }
                else
                {
                    
                    HP = HP+(int)(MaxHP * .15);
                }
                if ((Stamina + 40) >= 100)
                {
                    Stamina = 100;
                }
                else
                {
                    Stamina =+ 40;
                }
            }
            
            
            RaiseMessage("You taken a rest and regain health and replenshed stamina");
            if(CurrentMonster != null)
            {
                if(CurrentMonster.HP >= 0)
                {
                    double crit = CurrentMonster.MaxDmg * 1.5;
                    int damageToPlayer = RandomNumberGen.NumberBetween(10, (int)crit);
                    RaiseMessage("The " + CurrentMonster.Name + " did " + damageToPlayer + " points of damage.");
                    HP -= damageToPlayer;
                    if (HP <= 0)
                    {
                        RaiseMessage("The " + CurrentMonster.Name + " killed you.");
                        Gold = Gold - (int)(0.05 * Gold);
                        MoveHome();
                    }
                }
            }
        }
        private void MoveHome()
        {
            MoveTo(World.LocationByID(World.Location_ID_Camp));
            HP = MaxHP;
            Stamina = 100;
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private void LootTheCurrentMonster()
        {
            RaiseMessage("");
            RaiseMessage("You defeated the " + CurrentMonster.Name);
            RaiseMessage("You receive " + CurrentMonster.RewardEXP + " exp.");
            RaiseMessage("You receive " + CurrentMonster.Gold + " gold.");

            AddEXP(CurrentMonster.RewardEXP);
            Gold += CurrentMonster.Gold;

            foreach(Inventory inventory in CurrentMonster.lootItems)
            {
                additemtoinventor(inventory.Details);

                RaiseMessage(string.Format("You loot {0} {1}", inventory.Quantity, inventory.Description));
            }
            RaiseMessage("");
        }
        private void HealPlayer(int hpToHeal)
        {
            HP = Math.Min(HP + hpToHeal, MaxHP);
        }
        private void LetTheMonsterAttack()
        {
            int monsterhit = RandomNumberGen.NumberBetween(0, CurrentMonster.Sight+level);
            if(monsterhit>=(int)(Speed/2))
            { 
                int damagetoPlayer = RandomNumberGen.NumberBetween(2, CurrentMonster.MaxDmg);
                RaiseMessage("The " + CurrentMonster.Name + " did " + damagetoPlayer + " of damage.");
                HP -= damagetoPlayer;
                if(IsDead)
                {
                RaiseMessage("You have fainted from the " + CurrentMonster.Name + " attack.");
                MoveHome();
                Gold = Gold - (int)(0.05*Gold);
                }
            }
            else
            {
                RaiseMessage("The " + CurrentMonster.Name + " missed.");
            }
        }
        private void AddEXP(int exp)
        {
            int playerlvlup = 100;
            EXP += exp;
            if(EXP >=playerlvlup)
            {
                RaiseMessage("Level Up!");
            }
            if (EXP >= playerlvlup)
            {
                if(level == 1)
                {
                    playerlvlup = 300;
                }
                else if(level == 2)
                {
                    playerlvlup = 900;
                }
                else if (level == 3)
                {
                    playerlvlup = 1800;
                }
                else if (level == 4)
                {
                    playerlvlup = 3600;
                }
                else if (level == 5)
                {

                }
                else if (level == 6)
                {

                }
                else if (level == 7)
                {

                }
                else if (level == 8)
                {

                }
                else if (level == 9)
                {

                }
                else if (level == 10)
                {

                }
                else if (level == 11)
                {

                }
                else if (level == 12)
                {

                }
                else if (level == 13)
                {

                }
                else if (level == 14)
                {

                }
                else if (level == 15)
                {

                }
            }
            
        }
        private void GivePlayerQuestRewards(Quest quest)
        {
            RaiseMessage("");
            RaiseMessage("Thank you for " + quest.Description);
            RaiseMessage("Take this:");
            RaiseMessage(quest.rewardEXP + " EXP");
            RaiseMessage(quest.Gold + " gold");
            RaiseMessage(quest.rewardItem.Name, true);

            AddEXP(quest.rewardEXP);
            Gold += quest.Gold;

            RemoveQuestCompletuionItems(quest);
            additemtoinventor(quest.rewardItem);

            MarkQuestAsDone(quest);
        }

        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public static Player CreateDefaultPlayer()
        {
            Player player = new Player(100, 100, 100, 5, 0, 10, 10, 10, 10, 10, 10, 5);
            player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_GODSWORD), 1));//GET RID OF
            player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_Sword), 1));
            player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_hedgeapple), 1)); // GET RID OF
            player.CurrentLocation = World.LocationByID(World.Location_ID_Camp);
            return player;
        }
        public static Player CreatePlayerfromDatabase (int hp, int maxhp, int gold, int exp, int currentlocation)
        {
            Player player = new Player(hp, maxhp,100, gold, exp,10,10,10,10,10,10,5);
            player.MoveTo(World.LocationByID(currentlocation));
            return player;
        }
        public static Player createplayerformxmlString (string xmlPlayerData)
        {
            try
            {
                XmlDocument playerdata = new XmlDocument();

                playerdata.LoadXml(xmlPlayerData);

                int HP = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/HP").InnerText);
                int MaxHP = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/MaxHP").InnerText);
                int gold = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/Gold").InnerText);
                int exp = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/EXP").InnerText);

                Player player = new Player(HP, MaxHP,100, gold, exp,10,10,10,10,10,10,5);

                int currentLocationID = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/CurrentLocation").InnerText);
                player.CurrentLocation = World.LocationByID(currentLocationID);
                if(playerdata.SelectSingleNode(".Player/Stats/CurrentWeapon") != null)
                {
                    int currentWeaponID = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/CurrentWeapon").InnerText);
                    player.CurrentWeapon = (Weapon)World.ItemByID(currentWeaponID);
                }
                foreach(XmlNode node in playerdata.SelectNodes("/Player/Inventory/Inventory"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);
                    int quantity = Convert.ToInt32(node.Attributes["Quantity"].Value);
                    for (int i = 0; i < quantity; i++)
                    {
                        player.additemtoinventor(World.ItemByID(id));
                    }
                }
                foreach(XmlNode node in playerdata.SelectNodes("/Player/PlayerQuest/PlayerQuest"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);
                    bool iscompleted = Convert.ToBoolean(node.Attributes["IsCompleted"].Value);

                    PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(id));
                    playerQuest.IsCompleted = iscompleted;
                    player.Quest.Add(playerQuest);
                }
                return player;
            }
            catch
            {
                return CreateDefaultPlayer();
            }
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public bool HasRequiredItemToEnter(Location location)
        {
            if(location.Itemrequiredtoenter == null)
            {
                return true;
            }
            return Inventory.Any(ii => ii.Details.ID == location.Itemrequiredtoenter.ID);
        }
        public bool hasallquestcompleted(Quest quest)
        {
            foreach(QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                if(!Inventory.Any(ii=>ii.Details.ID==qci.Details.ID &&ii.Quantity>=qci.Quanity))
                {
                    return false;
                }
            }
            return true;
        }
        private bool HasRequiredItemToEnterThisLcoation(Location location)
        {
            if(location.NoReqiredItemToEnter)
            {
                return true;
            }

            return Inventory.Any(ii => ii.Details.ID == location.Itemrequiredtoenter.ID);
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public void MarkQuestAsDone(Quest quest)
        {
            PlayerQuest playerQuest = Quest.SingleOrDefault(pq => pq.Details.ID == quest.ID);
            if(playerQuest != null)
            {
                playerQuest.IsCompleted = true;
            }
        }
        private void GiveQuestToPlayer(Quest quest)
        {
            RaiseMessage("You receive the " + quest.Name + " quest.");
            RaiseMessage(quest.Description);
            RaiseMessage("To complete it, return with:");
            foreach(QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                RaiseMessage(string.Format("{0} {1}", qci.Quanity, qci.Quanity == 1 ? qci.Details.Name : qci.Details.NamePlural));
            }
            RaiseMessage("");
            Quest.Add(new PlayerQuest(quest));
        }
        private bool PlayerHasAllQuestCompletionItemFor(Quest quest)
        {
            foreach(QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                if(!Inventory.Any(ii => ii.Details.ID == qci.Details.ID && ii.Quantity >= qci.Quanity))
                {
                    return false;
                }
            }
            return true;
        }
        private void RemoveQuestCompletuionItems(Quest quest)
        {
            foreach(QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == qci.Details.ID);
                if(item != null)
                {
                    RemoveItemFromInventory(item.Details, qci.Quanity);
                }
            }
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private void SetTheCurrentMonsterForTheCurrentLocation(Location location)
        {
            CurrentMonster = location.NewInstanceOfMonsterLivingHere();
            if(CurrentMonster != null)
            {
                RaiseMessage("You see a " + CurrentMonster.Name);
            }
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private bool PlayerDoesNotHaveTheRequiredItemToEnter (Location location)
        {
            return !HasRequiredItemToEnter(location);
        }
        private bool PlayerDoesNotHaveThisQuest (Quest quest)
        {
            return Quest.All(pq => pq.Details.ID != quest.ID);
        }
        private bool PlayerHasNotCompleted (Quest quest)
        {
            return Quest.Any(pq => pq.Details.ID == quest.ID && !pq.IsCompleted);
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public string Toxmlstring()
        {
            XmlDocument playerData = new XmlDocument();

            XmlNode player = playerData.CreateElement("Player");
            playerData.AppendChild(player);

            XmlNode stats = playerData.CreateElement("Stats");
            player.AppendChild(stats);

            XmlNode HP = playerData.CreateElement("HP");
            HP.AppendChild(playerData.CreateTextNode(this.HP.ToString()));
            stats.AppendChild(HP);

            XmlNode maxHP = playerData.CreateElement("MaxHP");
            maxHP.AppendChild(playerData.CreateTextNode(this.MaxHP.ToString()));
            stats.AppendChild(maxHP);

            XmlNode gold = playerData.CreateElement("Gold");
            gold.AppendChild(playerData.CreateTextNode(this.Gold.ToString()));
            stats.AppendChild(gold);

            XmlNode exp = playerData.CreateElement("EXP");
            exp.AppendChild(playerData.CreateTextNode(this.EXP.ToString()));
            stats.AppendChild(exp);

            XmlNode currentLocation = playerData.CreateElement("CurrentLocation");
            currentLocation.AppendChild(playerData.CreateTextNode(this.CurrentLocation.ID.ToString()));
            stats.AppendChild(currentLocation);

            if(CurrentWeapon != null)
            {
                XmlNode currentWeapon = playerData.CreateElement("CurrentWeapon");
                currentWeapon.AppendChild(playerData.CreateTextNode(this.CurrentWeapon.ID.ToString()));
                stats.AppendChild(currentWeapon);
            }

            XmlNode inventoryItems = playerData.CreateElement("InventoryItem");
            player.AppendChild(inventoryItems);
            foreach(Inventory item in this.Inventory)
            {
                XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

                XmlAttribute idattribute = playerData.CreateAttribute("ID");
                idattribute.Value = item.Details.ID.ToString();
                inventoryItem.Attributes.Append(idattribute);

                XmlAttribute quantityAttribute = playerData.CreateAttribute("Quantity");
                quantityAttribute.Value = item.Quantity.ToString();
                inventoryItem.Attributes.Append(quantityAttribute);

                inventoryItem.AppendChild(inventoryItem);
            }
            XmlNode playerQuest = playerData.CreateElement("PlayerQuests");
            player.AppendChild(playerQuest);

            foreach(PlayerQuest quest in this.Quest)
            {
                XmlNode playerquest = playerData.CreateElement("PlayerQuest");
                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = quest.Details.ID.ToString();
                playerquest.Attributes.Append(idAttribute);

                XmlAttribute iscompletedAttribute = playerData.CreateAttribute("Iscompleted");
                iscompletedAttribute.Value = quest.IsCompleted.ToString();
                playerquest.Attributes.Append(iscompletedAttribute);

                playerquest.AppendChild(playerquest);
            }
            return playerData.InnerXml;
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
    }
}
