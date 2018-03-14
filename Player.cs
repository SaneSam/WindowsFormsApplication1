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
        private int _endurance;
        private int _speed;
        private int _sight;
        private int _intelligence;
        private int _level;
        private Location _currentlocation;
        private Monster _currentMonster;
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
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged("Levelup");
            }
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
        public int Endurance
        {
            get { return _endurance; }
            set
            {
                _endurance = value;
                OnPropertyChanged("Stats");
            }
        }
        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnPropertyChanged("Stats");
            }
        }
        public int Sight
        {
            get { return _sight; }
            set
            {
                _sight = value;
                OnPropertyChanged("Stats");
            }
        }
        public int Intelligence
        {
            get { return _intelligence; }
            set
            {
                _intelligence = value;
                OnPropertyChanged("Stats");
            }
        }
        public int AC { get; set; }
        public int playerlvlup = 100;
        public int levels = 1;
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public Weapon CurrentWeapon { get; set; }
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                _currentMonster = value;
                OnPropertyChanged("CurrentMonster");
            }
        }
        public BindingList<Inventory> Inventory { get; set; }
        public BindingList<PlayerQuest> Quest { get; set; }
        public Location CurrentLocation
        {
            get { return _currentlocation; }
            set
            {
                _currentlocation = value;
                OnPropertyChanged("CurrentLocation");
            }
        }

        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private Player(int hp, int maxhp, int stamina, int gold, int exp, int strength, int endurance, int speed, int sight, int intelligence, int ArmorClass) : base(hp, maxhp, stamina)
        {
            Stamina = stamina;
            Gold = gold;
            EXP = exp;
            Strength = strength;
            Endurance = endurance;
            Speed = speed;
            Sight = sight;
            Intelligence = intelligence;
            AC = ArmorClass;
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
            if (item is Weapon)
            {
                OnPropertyChanged("Weapon");
            }
            if (item is Healing)
            {
                OnPropertyChanged("Potion");
            }
        }
        public void RemoveItemFromInventory(_items itemToRemove, int quantity = 1)
        {
            Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToRemove.ID);
            if (item != null)
            {
                item.Quantity -= quantity;
                if (item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }
                raiseInventoryChangedEvent(itemToRemove);
            }
        }
        public void additemtoinventor(_items itemtoadd, int quantity = 1)
        {
            Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemtoadd.ID);
            if (item == null)
            {
                Inventory.Add(new Inventory(itemtoadd, quantity));
            }
            else
            {
                item.Quantity += quantity;
            }
            raiseInventoryChangedEvent(itemtoadd);
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public void UsePotion(Healing potion)
        {
            RaiseMessage("You drink a " + potion.Name);

            HealPlayer(potion.AmountToHeal);
            if (potion.Something == true)
            {
                CheckPotion(potion);
            }
            RemoveItemFromInventory(potion);
            if (CurrentMonster != null)
            {
                LetTheMonsterAttack();
            }

        }
        public void CheckPotion (Healing potion)
        {
            if(potion.ID == 8)
            {
                RaiseMessage("You feel woozy from drinking this.");
                List<int> listNumbers = new List<int>();
                int number = 0;
                int shuffle = -50 + (Strength + Endurance + Speed + Intelligence + Sight);
                for (int i = 5; i != 0; i--)
                {
                    do
                    {
                        number = RandomNumberGen.NumberBetween(1, 5);
                    } while (listNumbers.Contains(number));
                    listNumbers.Add(number);
                    if (shuffle <= 0)
                    {
                        shuffle = 0;
                    }
                    int random = RandomNumberGen.NumberBetween(0, shuffle);
                    if (random >= 100)
                    {
                        random = 100;
                    }
                    else if (random <= 0)
                    {
                        random = 1;
                    }
                    if (number == 5)
                    {
                        Strength = random + 10;
                    }
                    else if (number == 4)
                    {
                        Endurance = random + 10;
                    }
                    else if (number == 3)
                    {
                        Speed = random + 10;
                    }
                    else if (number == 2)
                    {
                        Sight = random + 10;
                    }
                    else if (number == 1)
                    {
                        Intelligence = random + 10;
                    }
                    shuffle = shuffle - random;
                }
            }
            //
            if(potion.ID == 9)
            {
                RaiseMessage("This should protect way better than those rags.");
                AC = 10;
            }
            
        }
        public void UseWeapon(Weapon weapon)
        {
            
            RaiseMessage(CurrentMonster.Name);
            int damageToMonster = RandomNumberGen.NumberBetween(weapon.MinDmg, weapon.MaxDmg);
            int dice = RandomNumberGen.NumberBetween(1, 20);
            int dice2 = dice + (int)(Sight / 10);
            if (dice == 20)
            {
                CurrentMonster.HP -= damageToMonster * 2;
                RaiseMessage("Critial Hit!");
                RaiseMessage("You hit the " + CurrentMonster.Name + " for " + (damageToMonster * 2) + ".");
            }
            else if (dice2 >= CurrentMonster.Ac && dice != 0)
            {
                if(weapon.DmgType == CurrentMonster.Weakness)
                {
                    if(weapon.DmgType == "strike")
                    {
                        CurrentMonster.HP -= (damageToMonster + (int)(Strength / 2));
                        RaiseMessage("You hit the " + CurrentMonster.Name + " for " + (damageToMonster + (int)(Strength / 5)) + ".");
                    }
                    else if(weapon.DmgType == "magic")
                    {
                        CurrentMonster.HP -= (damageToMonster + (int)(Intelligence / 4));
                        RaiseMessage("You hit the " + CurrentMonster.Name + " for " + (damageToMonster + (int)(Intelligence / 4)) + ".");
                    }
                    else if(weapon.DmgType == "pericing")
                    {
                        CurrentMonster.HP -= (damageToMonster + (int)(Speed / 4));
                        RaiseMessage("You hit the " + CurrentMonster.Name + " for " + (damageToMonster + (int)(Speed / 4)) + ".");
                    }
                    else if(weapon.DmgType == "blunt")
                    {
                        CurrentMonster.HP -= (damageToMonster + (int)(Endurance / 4));
                        RaiseMessage("You hit the " + CurrentMonster.Name + " for " + (damageToMonster + (int)(Endurance / 4)) + ".");
                    }
                }
                else if(weapon.DmgType == "crit")
                {
                    CurrentMonster.HP -= (int)(damageToMonster*1.5);
                    RaiseMessage("You hit the " + CurrentMonster.Name + " for " + (int)(damageToMonster*1.5) + ".");
                }
                else
                {
                    CurrentMonster.HP -= (damageToMonster );
                    RaiseMessage("You hit the " + CurrentMonster.Name + " for " + damageToMonster + ".");
                }
                
            }
            else
            {
                RaiseMessage("You missed the " + CurrentMonster.Name);
            }
            if (CurrentMonster.IsDead)
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
        private void RaiseMessage(string message, bool addExtraNewLine = false)
        {
            if (OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(message, addExtraNewLine));
            }

        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public void MoveTo(Location location)
        {
            if (Stamina <= 0)
            {
                Stamina = 0;
            }
            if (!HasRequiredItemToEnter(location))
            {
                RaiseMessage("You must have a " + location.Itemrequiredtoenter.Name + " to enter this location.");
                return;
            }
            CurrentLocation = location;
            if (location.hasAQuest)
            {
                int quest = 1;
                if (PlayerDoesNotHaveThisQuest(location.QuestAvailableHere))
                {
                    GiveQuestToPlayer(World.QuestByID(quest));
                }
                else
                {
                    if (PlayerHasNotCompleted(World.QuestByID(quest)) && PlayerHasAllQuestCompletionItemFor(World.QuestByID(quest)))
                    {
                        GivePlayerQuestRewards(World.QuestByID(quest));
                        quest++;
                        if (World.QuestByID(quest) != null)
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
            if (Stamina <= 0 || HP <= 0)
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
            if (Stamina <= 0 || HP <= 0)
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
            if (Stamina <= 0 || HP <= 0)
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
            if (Stamina <= 0 || HP <= 0)
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
                if(Stamina <= 100+((int)Speed/4))
                {
                    Stamina = 100 + ((int)Speed / 5);
                }

            }
            else
            {
                if (MaxHP <= (HP + (int)(MaxHP * .12)))
                {
                    HP = MaxHP;
                }
                else
                {

                    HP = HP + (int)(MaxHP * .12);
                }
                if ((Stamina + 40) >= 100)
                {
                    Stamina = 100;
                }
                else
                {
                    Stamina += 40;
                }
            }


            RaiseMessage("You had a rest and regain health and replenshed stamina");
            if (CurrentMonster != null)
            {
                if (CurrentMonster.HP >= 0)
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
        public void MoveHome()
        {
            MoveTo(World.LocationByID(World.Location_ID_Camp));
            HP = MaxHP;
            Stamina = 100;
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private void LootTheCurrentMonster()
        {
            RaiseMessage("");
            RaiseMessage("The " + CurrentMonster.Name + " falls to your strength.");
            RaiseMessage("You gain " + CurrentMonster.RewardEXP + " exp.");
            RaiseMessage("You receive " + CurrentMonster.Gold + " gold.");

            AddEXP(CurrentMonster.RewardEXP);
            Gold += CurrentMonster.Gold;

            foreach (Inventory inventory in CurrentMonster.lootItems)
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
            int monsterhit = RandomNumberGen.NumberBetween(0, CurrentMonster.Sight + level);
            if (monsterhit >= (int)(Speed / 5))
            {
                int damagetoPlayer = (RandomNumberGen.NumberBetween((int)(CurrentMonster.MaxDmg/4), CurrentMonster.MaxDmg)-AC);
                RaiseMessage("The " + CurrentMonster.Name + " did " + damagetoPlayer + " of damage.");
                HP -= damagetoPlayer;
                if (IsDead)
                {
                    RaiseMessage("You have fainted from the " + CurrentMonster.Name + " attack.");
                    MoveHome();
                    Gold = Gold - (int)(0.05 * Gold);
                }
            }
            else
            {
                RaiseMessage("The " + CurrentMonster.Name + " missed.");
            }
        }
        private void AddEXP(int exp)
        {
            double multiplyer = 1;
            if (Intelligence >= 50)
            {
                multiplyer = 1.5;
            }
            else if (Intelligence == 100)
            {
                multiplyer = 2;
            }

            EXP += (int)(exp * multiplyer);
            if (EXP >= playerlvlup)
            {
                if (EXP >= playerlvlup)
                {
                    RaiseMessage("Level Up!");
                }
                if (levels == 1 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 2;
                    playerlvlup = 300;

                }
                else if (levels == 2 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 3;
                    playerlvlup = 600;
                }
                else if (levels == 3 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 4;
                    playerlvlup = 900;
                }
                else if (levels == 4 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 5;
                    playerlvlup = 1200;
                }
                else if (levels == 5 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 6;
                    playerlvlup = 1500;
                }
                else if (levels == 6 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 7;
                    playerlvlup = 1800;
                }
                else if (levels == 7 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 8;
                    playerlvlup = 2100;
                }
                else if (levels == 8 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 9;
                    playerlvlup = 2400;
                }
                else if (levels == 9 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 10;
                    playerlvlup = 2700;
                }
                else if (levels == 10 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 11;
                    playerlvlup = 3000;
                }
                else if (levels == 11 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 12;
                    playerlvlup = 3300;
                }
                else if (levels == 12 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 13;
                    playerlvlup = 3600;
                }
                else if (levels == 13 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 14;
                    playerlvlup = 3900;
                }
                else if (levels == 14 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 15;
                    playerlvlup = 4200;
                }
                else if (levels == 15 && playerlvlup <= EXP)
                {
                    EXP -= playerlvlup;
                    levels = 16;
                    playerlvlup = 4500;
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
            Player player = new Player(100, 100, 100, 10, 0, 10, 10, 10, 10, 10, 5);
            player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_Bow), 1));
            player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_GODSWORD), 1));
            player.CurrentLocation = World.LocationByID(World.Location_ID_Camp);
            return player;
            /*if(clas == "I")
            {
                Player playerI = new Player(120,120,100,20,0,13,10,12,11,10,7);
                player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_Dagger), 1));
                player.CurrentLocation = World.LocationByID(World.Location_ID_Camp);
                return playerI;

            }
            else if(clas == "M")
            {
                Player playerM = new Player(100, 100, 140, 5, 0, 10, 10, 13, 12, 16, 4);
                player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_Staff), 1));
                player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_Dagger), 1));
                player.CurrentLocation = World.LocationByID(World.Location_ID_Camp);
                return playerM;
            }
            else if(clas == "C")
            {
                Player playerC = new Player(110, 110, 110, 10, 0, 10, 10, 14, 13, 11, 6);
                player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_Bow), 1));
                player.Inventory.Add(new Inventory(World.ItemByID(World.Item_ID_Dagger), 1));
                player.CurrentLocation = World.LocationByID(World.Location_ID_Camp);
                return playerC;
            }*/
            
                
            
            
        }
        public static Player CreatePlayerfromDatabase(int hp, int maxhp, int gold, int exp, int currentlocation)
        {
            Player player = new Player(hp, (maxhp+10), 100, gold, exp, 10, 10, 10, 10, 10, 5);
            player.MoveTo(World.LocationByID(currentlocation));
            return player;
        }
        public static Player createplayerformxmlString(string xmlPlayerData)
        {
            try
            {
                XmlDocument playerdata = new XmlDocument();

                playerdata.LoadXml(xmlPlayerData);

                int HP = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/HP").InnerText);
                int MaxHP = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/MaxHP").InnerText);
                int gold = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/Gold").InnerText);
                int exp = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/EXP").InnerText);

                Player player = new Player(HP, MaxHP, 100, gold, exp, 10, 10, 10, 10, 10, 5);

                int currentLocationID = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/CurrentLocation").InnerText);
                player.CurrentLocation = World.LocationByID(currentLocationID);
                if (playerdata.SelectSingleNode(".Player/Stats/CurrentWeapon") != null)
                {
                    int currentWeaponID = Convert.ToInt32(playerdata.SelectSingleNode("/Player/Stats/CurrentWeapon").InnerText);
                    player.CurrentWeapon = (Weapon)World.ItemByID(currentWeaponID);
                }
                foreach (XmlNode node in playerdata.SelectNodes("/Player/Inventory/Inventory"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);
                    int quantity = Convert.ToInt32(node.Attributes["Quantity"].Value);
                    for (int i = 0; i < quantity; i++)
                    {
                        player.additemtoinventor(World.ItemByID(id));
                    }
                }
                foreach (XmlNode node in playerdata.SelectNodes("/Player/PlayerQuest/PlayerQuest"))
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
            if (location.Itemrequiredtoenter == null)
            {
                return true;
            }
            return Inventory.Any(ii => ii.Details.ID == location.Itemrequiredtoenter.ID);
        }
        public bool hasallquestcompleted(Quest quest)
        {
            foreach (QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                if (!Inventory.Any(ii => ii.Details.ID == qci.Details.ID && ii.Quantity >= qci.Quanity))
                {
                    return false;
                }
            }
            return true;
        }
        private bool HasRequiredItemToEnterThisLcoation(Location location)
        {
            if (location.NoReqiredItemToEnter)
            {
                return true;
            }

            return Inventory.Any(ii => ii.Details.ID == location.Itemrequiredtoenter.ID);
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        public void MarkQuestAsDone(Quest quest)
        {
            PlayerQuest playerQuest = Quest.SingleOrDefault(pq => pq.Details.ID == quest.ID);
            if (playerQuest != null)
            {
                playerQuest.IsCompleted = true;
            }
        }
        private void GiveQuestToPlayer(Quest quest)
        {
            RaiseMessage("You receive the " + quest.Name + " quest.");
            RaiseMessage(quest.Description);
            RaiseMessage("To complete it, return with:");
            foreach (QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                RaiseMessage(string.Format("{0} {1}", qci.Quanity, qci.Quanity == 1 ? qci.Details.Name : qci.Details.NamePlural));
            }
            RaiseMessage("");
            Quest.Add(new PlayerQuest(quest));
        }
        private bool PlayerHasAllQuestCompletionItemFor(Quest quest)
        {
            foreach (QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                if (!Inventory.Any(ii => ii.Details.ID == qci.Details.ID && ii.Quantity >= qci.Quanity))
                {
                    return false;
                }
            }
            return true;
        }
        private void RemoveQuestCompletuionItems(Quest quest)
        {
            foreach (QuestCompletedItem qci in quest.QuestCompletedItem)
            {
                Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == qci.Details.ID);
                if (item != null)
                {
                    RemoveItemFromInventory(item.Details, qci.Quanity);
                }
            }
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private void SetTheCurrentMonsterForTheCurrentLocation(Location location)
        {
            CurrentMonster = location.NewInstanceOfMonsterLivingHere();
            if (CurrentMonster != null)
            {
                RaiseMessage("You see a " + CurrentMonster.Name);
            }
        }
        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
        private bool PlayerDoesNotHaveTheRequiredItemToEnter(Location location)
        {
            return !HasRequiredItemToEnter(location);
        }
        private bool PlayerDoesNotHaveThisQuest(Quest quest)
        {
            return Quest.All(pq => pq.Details.ID != quest.ID);
        }
        private bool PlayerHasNotCompleted(Quest quest)
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

            if (CurrentWeapon != null)
            {
                XmlNode currentWeapon = playerData.CreateElement("CurrentWeapon");
                currentWeapon.AppendChild(playerData.CreateTextNode(this.CurrentWeapon.ID.ToString()));
                stats.AppendChild(currentWeapon);
            }

            XmlNode inventoryItems = playerData.CreateElement("InventoryItem");
            player.AppendChild(inventoryItems);
            foreach (Inventory item in this.Inventory)
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

            foreach (PlayerQuest quest in this.Quest)
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