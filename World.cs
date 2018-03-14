using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class World
    {
        public static readonly List<_items> _items = new List<_items>();
        public static readonly List<Monster> _monsters = new List<Monster>();
        public static readonly List<Quest> _quests = new List<Quest>();
        public static readonly List<Location>_locations = new List<Location>();

        public const int UNSELLABLE_ITEM_PRICE = -1;
        public const int Item_ID_Sword = 1; 
        public const int Item_ID_Bow = 2; 
        public const int Item_ID_Staff = 3; 
        public const int Item_ID_hedgeapple = 4; 
        public const int Item_ID_arcobot_Fang = 5; 
        public const int Item_ID_Chunks_of_leather = 6; 
        public const int Item_ID_Ball_And_Chain = 7;
        public const int Item_ID_Mystery_Potion = 8;
        public const int Item_ID_Lether_Armor = 9;
        public const int Item_ID_Broadsword = 10;
        public const int Item_ID_Magic_Moss = 11;
        public const int Item_ID_Gem = 12;
        public const int Item_ID_Doctor_Scalpel = 13;
        public const int Item_ID_Dagger = 14;
        public const int Item_ID_GODSWORD = 9999; //beta weapon

        public const int Monster_ID_Arcobot = 1;
        public const int Monster_ID_Bandit = 2;
        public const int Monster_ID_Man_Eater = 3;
        public const int Monster_ID_Ent = 4;
        public const int Monster_ID_2BearsHighFiving = 5;
        public const int Monster_ID_BORIS = 666;
        public const int Monster_ID_nymph = 6;
        public const int Monster_ID_Basilisks = 7;
        public const int Monster_ID_Gnomes = 8;

        public const int Quest_ID_Clear_Kill_5_arcobot = 1;
        public const int Quest_ID_Clear_retreive_5_Chucks_of_lether = 2;
        public const int Quest_ID_Clear_Prison_Riot = 3;
        public const int Quest_ID_Clear_Kill_A_Small_Man = 4;

        public const int Location_ID_Camp = 1;
        public const int Location_ID_Forst = 2; 
        public const int Location_ID_Jungle = 3;
        public const int Location_ID_Mountain = 4;
        public const int Location_ID_Pasture = 5; 
        public const int Location_ID_Cave = 6;
        public const int Location_ID_DeepCave = 7;
        public const int Location_ID_Water = 8;
        public const int Location_ID_Beach = 9;

        

        public const int Location_ID_Town = 10;
        public const int Location_ID_LostsSHOP = 010;
        public const int Location_ID_LostsBar = 0010;

        public const int Location_ID_Bandit_Encampment = 11;

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuest();
            PopulateLocation();
        }

        private static void PopulateItems()
        {
            _items.Add(new Weapon(Item_ID_Sword, "Damaged Captain's Sword", "Damaged Captain's Swords", 15, 23,130,"strike"));
            _items.Add(new Weapon(Item_ID_Bow, "Old Bow", "Old Bows", 12, 17,110,"pericing"));
            _items.Add(new Weapon(Item_ID_Staff, "Rotten staff", "Rotten staffs", 8, 15,120,"magic"));
            _items.Add(new Weapon(Item_ID_GODSWORD, "GOD SWORD", "GOD SWORDS", 9998, 9999,-1,"strike"));
            _items.Add(new Weapon(Item_ID_Ball_And_Chain, "Boris's Ball", "Boris's balls", 14, 18, -1,"blunt"));
            _items.Add(new Healing(Item_ID_Mystery_Potion, "Mystery Potion 7", "Mystery Potion 7's",0,30,true));
            _items.Add(new Healing(Item_ID_hedgeapple, "Hedge apple", "Hedge apples",5,6,false)); 
            _items.Add(new _items(Item_ID_arcobot_Fang, "Arcobot's Fang", "Arcobot's Fangs",4));
            _items.Add(new _items(Item_ID_Chunks_of_leather, "cloth", "chunks of cloth",6));
            _items.Add(new Healing(Item_ID_Lether_Armor, "Armor", "Armor", 0, 500, true));
            _items.Add(new Weapon(Item_ID_Broadsword, "Broadsword", "Broadswords", 9, 17, 220, "strike"));
            _items.Add(new Healing(Item_ID_Magic_Moss, "Magic Moss", "Magic Mosses", 1000, -1, true));
            _items.Add(new _items(Item_ID_Gem, "Gem", "Gems", 90));
            _items.Add(new Weapon(Item_ID_Doctor_Scalpel, "Doc's Scalpel", "Doc's Scalpel", 7, 13, -1, "crit"));
            _items.Add(new Weapon(Item_ID_Dagger, "Broken Dagger", "Na", 5, 7, 10, "crit"));
        }

        private static void PopulateMonsters()
        {
            Monster AcroBot = new Monster(Monster_ID_Arcobot,"Arcobot", 12,10,2,50,50,999,4,8,"pericing");
            AcroBot.LootTable.Add(new LootItem(ItemByID(Item_ID_arcobot_Fang),40,false));

            Monster Bandit = new Monster(Monster_ID_Bandit, "Bandit", 21, 24, 7, 75, 75,999,6,9,"strike");
            Bandit.LootTable.Add(new LootItem(ItemByID(Item_ID_Chunks_of_leather), 40, false));

            Monster ManEater = new Monster(Monster_ID_Man_Eater, "Man Eater", 37, 52, 4, 125, 125, 999,12,4,"strike");
            ManEater.LootTable.Add(new LootItem(ItemByID(Item_ID_hedgeapple), 100, true));

            Monster Ent = new Monster(Monster_ID_Ent, "Ent", 42, 210, 2, 200, 200, 999,15,12,"magic");

            Monster bearshighfiving = new Monster(Monster_ID_2BearsHighFiving, "Bear", 32, 45, 14, 100, 100, 999,1,8,"blunt");

            Monster BORIS = new Monster(Monster_ID_BORIS, "Boris, The Prisoner", 52, 130, 0, 300, 300, 999, 20, 15,"strike");
            BORIS.LootTable.Add(new LootItem(ItemByID(Item_ID_Ball_And_Chain), 100, true));

            Monster nymph = new Monster(Monster_ID_nymph, "Fair Lady", 23, 50, 2, 90, 90, 999, 12, 18, "strike");
            nymph.LootTable.Add(new LootItem(ItemByID(Item_ID_Chunks_of_leather), 100, true));
            nymph.LootTable.Add(new LootItem(ItemByID(Item_ID_Magic_Moss), 10, false));

            Monster Basilisk = new Monster(Monster_ID_Basilisks, "Basilisk", 22, 200, 50, 200, 200, 999, 20, 5, "pericing");
            Basilisk.LootTable.Add(new LootItem(ItemByID(Item_ID_Broadsword), 20, true));

            Monster Gnome = new Monster(Monster_ID_Gnomes, "Gnome", 7, 30, 20, 100, 100, 999, 17, 15, "magic");
            Gnome.LootTable.Add(new LootItem(ItemByID(Item_ID_Gem), 70, true));

            _monsters.Add(Basilisk);
            _monsters.Add(Gnome);
            _monsters.Add(nymph);
            _monsters.Add(BORIS);
            _monsters.Add(AcroBot);
            _monsters.Add(Bandit);
            _monsters.Add(ManEater);
            _monsters.Add(Ent);
            _monsters.Add(bearshighfiving);
        }
        private static void PopulateQuest()
        {
            Quest KillArcoBots = new Quest(Quest_ID_Clear_Kill_5_arcobot, "Doctor's calling", "Go to the forest bring back 3 iron fangs.",150,10);
            KillArcoBots.QuestCompletedItem.Add(new QuestCompletedItem(ItemByID(Item_ID_arcobot_Fang), 3));
            KillArcoBots.rewardItem = ItemByID(Item_ID_Doctor_Scalpel);

            Quest Protector = new Quest(Quest_ID_Clear_retreive_5_Chucks_of_lether, "Fashion Police", "kill off those bandits and retrieve 10 chuncks of leather for us.",225,10);
            Protector.QuestCompletedItem.Add(new QuestCompletedItem(ItemByID(Item_ID_Chunks_of_leather), 10));
            Protector.rewardItem = ItemByID(Item_ID_Lether_Armor);

            Quest Prison_Riot = new Quest(Quest_ID_Clear_Prison_Riot, "Find and Kill Borris", "Borris is a bandit, usually we deal with him but he keeps breaking out. If you can \"deal\" with him that be nice.", 300, 400);
            Prison_Riot.QuestCompletedItem.Add(new QuestCompletedItem(ItemByID(Item_ID_Ball_And_Chain), 1));
            Prison_Riot.rewardItem = ItemByID(Item_ID_Ball_And_Chain);

            Quest Shiny = new Quest(Quest_ID_Clear_Kill_A_Small_Man, "Romantic Helper", "I require a gem, to give my wife for our aniveristy.", 50, 150);
            Shiny.QuestCompletedItem.Add(new QuestCompletedItem(ItemByID(Item_ID_Gem), 1));

            _quests.Add(Shiny);
            _quests.Add(Prison_Riot);
            _quests.Add(KillArcoBots);
            _quests.Add(Protector);
        }
        private static void PopulateLocation()
        { //odds y<0 evens y>0 
            Location Camp = new Location(Location_ID_Camp, "Camp", "The makeshift place you call home... cozy",false);
            //
            Location Forest = new Location(Location_ID_Forst, "Forest", "The entrance to the forest from the pasture of your camp.", false);
            Forest.NewInstanceOfMonsterLivingHere();
            Forest.AddMonster(Monster_ID_Bandit, 20);
            Forest.AddMonster(Monster_ID_Arcobot, 20);
            Forest.AddMonster(Monster_ID_Ent, 1);
            Forest.MonsterAppearanceChance = 50;
            Location f1 = new Location(Location_ID_Forst, "Forest", "The entrance to the forest from the pasture of your camp.", false);//0,1
            f1.NewInstanceOfMonsterLivingHere();
            f1.AddMonster(Monster_ID_Bandit, 20);
            f1.AddMonster(Monster_ID_Arcobot, 20);
            f1.AddMonster(Monster_ID_Ent, 1);
            f1.MonsterAppearanceChance = 50;
            Location f2 = new Location(Location_ID_Forst, "Forest", "The entrance to the forest from the pasture of your camp.", false);
            f2.NewInstanceOfMonsterLivingHere();
            f2.AddMonster(Monster_ID_Bandit, 20);
            f2.AddMonster(Monster_ID_Arcobot, 20);
            f2.AddMonster(Monster_ID_Ent, 1);
            f2.MonsterAppearanceChance = 50;
            Location f3 = new Location(Location_ID_Forst, "Forest", "You notice a clearing of the trees, and see a cave; also seeing something enter the cave.", false);
            f3.NewInstanceOfMonsterLivingHere();
            f3.AddMonster(Monster_ID_Bandit, 20);
            f3.AddMonster(Monster_ID_Arcobot, 20);
            f3.AddMonster(Monster_ID_Ent, 5);
            f3.MonsterAppearanceChance = 50;
            Location f4 = new Location(Location_ID_Forst, "Forest", "seems like some of the trees, are gone.", false);
            f4.NewInstanceOfMonsterLivingHere();
            f4.AddMonster(Monster_ID_Bandit, 20);
            f4.AddMonster(Monster_ID_Arcobot, 20);
            f4.AddMonster(Monster_ID_Ent, 5);
            f4.MonsterAppearanceChance = 50;
            Location f5 = new Location(Location_ID_Forst, "Forest", "The plant life is more alive than usual.", false);
            f5.NewInstanceOfMonsterLivingHere();
            f5.AddMonster(Monster_ID_Bandit, 20);
            f5.AddMonster(Monster_ID_Arcobot, 20);
            f5.AddMonster(Monster_ID_Ent, 5);
            f5.MonsterAppearanceChance = 50;
            Location f6 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.", false);
            f6.NewInstanceOfMonsterLivingHere();
            f6.AddMonster(Monster_ID_Bandit, 20);
            f6.AddMonster(Monster_ID_Arcobot, 20);
            f6.MonsterAppearanceChance = 50;
            Location f7 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.", false);
            f7.NewInstanceOfMonsterLivingHere();
            f7.AddMonster(Monster_ID_Bandit, 20);
            f7.AddMonster(Monster_ID_Arcobot, 20);
            f7.AddMonster(Monster_ID_Ent, 10);
            f7.MonsterAppearanceChance = 50;
            Location f8 = new Location(Location_ID_Forst, "Forest", "There are hunting traps around the area, noting humans are around this area; hopfully friendly.", false);
            f8.NewInstanceOfMonsterLivingHere();
            f8.AddMonster(Monster_ID_Bandit, 20);
            f8.AddMonster(Monster_ID_Arcobot, 20);
            f8.MonsterAppearanceChance = 50;
            Location f10 = new Location(Location_ID_Forst, "Forest", "There are hunting traps around the area, noting humans are around this area; hopfully friendly.", false);
            f10.NewInstanceOfMonsterLivingHere();
            f10.AddMonster(Monster_ID_Bandit, 20);
            f10.AddMonster(Monster_ID_Arcobot, 20);
            f10.MonsterAppearanceChance = 50;
            Location f10a = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.", false);
            f10a.NewInstanceOfMonsterLivingHere();
            f10a.AddMonster(Monster_ID_Bandit, 20);
            f10a.AddMonster(Monster_ID_Arcobot, 20);
            f10a.MonsterAppearanceChance = 50;
            Location f6a = new Location(Location_ID_Forst, "Forest", "There are hunting traps around the area, noting humans are around this area; hopfully friendly.", false);
            f6a.NewInstanceOfMonsterLivingHere();
            f6a.AddMonster(Monster_ID_Bandit, 20);
            f6a.AddMonster(Monster_ID_Arcobot, 20);
            f6a.MonsterAppearanceChance = 50;
            Location f01 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.", false);
            f01.NewInstanceOfMonsterLivingHere();
            f01.AddMonster(Monster_ID_Bandit, 20);
            f01.AddMonster(Monster_ID_Arcobot, 20);
            f01.MonsterAppearanceChance = 50;
            Location f02 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.", false);
            f02.NewInstanceOfMonsterLivingHere();
            f02.AddMonster(Monster_ID_Bandit, 20);
            f02.AddMonster(Monster_ID_Arcobot, 20);
            f02.MonsterAppearanceChance = 50;
            Location f03 = new Location(Location_ID_Forst, "Forest", "Tall trees stand, but also large plants; noticing more bones around the forest", false);
            f03.NewInstanceOfMonsterLivingHere();
            f03.AddMonster(Monster_ID_Bandit, 20);
            f03.AddMonster(Monster_ID_Arcobot, 20);
            f03.AddMonster(Monster_ID_Ent, 10);
            f03.AddMonster(Monster_ID_Man_Eater, 5);
            f03.MonsterAppearanceChance = 50;
            //
            Location Jungle = new Location(Location_ID_Jungle, "Jungle", "Vines hang from the tree tops, barely little light come from the canopy; don't travel at night here.", false);
            //
            Location Mountain = new Location(Location_ID_Mountain, "Mountain", "Far pass the town and jungle is mountains it going to take a while to get their; and it looks cold bring a coat.", false);
            //

            Location Town = new Location(Location_ID_Town, "Lost Town", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.", false);

            Location t8a = new Location(Location_ID_Town, "Lost Town", "The Town seems pleasant, with its stalls for its little shops", false);

            Location t8b = new Location(Location_ID_Town, "Lost Town", "More people are going around the place, going from to where.", false);

            Location t8c = new Location(Location_ID_Town, "Lost Town", "Before you stands a large building, probally a court house.", false);

            Location Jail = new Location(Location_ID_Town, "Jail", "No one seems to be locked up yet.", false);
            Jail.MonsterAppearanceChance = 1;
            Jail.AddMonster(Monster_ID_BORIS, 1);
            Location GuildHall = new Location(Location_ID_Town, "Guild Hall", "Looks like some random jobs can be found here, look like you'll need more gold.", false);
            
            GuildHall.QuestAvailableHere = QuestByID(Quest_ID_Clear_retreive_5_Chucks_of_lether);
            
            Location Med = new Location(Location_ID_Town, "Clinic", "Rest here, if you feeling a bit down", false);
            Med.QuestAvailableHere = QuestByID(Quest_ID_Clear_Kill_5_arcobot);
            Location Shop = new Location(Location_ID_LostsSHOP, "Shop", "Welecome to the Shop", false); // Change later
            Vendortron9000 Vendy = new Vendortron9000("Vendy");
            Vendy.Additemtoinventory(ItemByID(Item_ID_Chunks_of_leather), 999);
            Vendy.Additemtoinventory(ItemByID(Item_ID_hedgeapple), 999);
            Vendy.Additemtoinventory(ItemByID(Item_ID_Sword), 99);
            Vendy.Additemtoinventory(ItemByID(Item_ID_Staff), 99);
            Vendy.Additemtoinventory(ItemByID(Item_ID_arcobot_Fang), 99);
            Vendy.Additemtoinventory(ItemByID(Item_ID_Bow), 99);
            
            Shop.Vending = Vendy;

            Location Bar = new Location(Location_ID_LostsBar, "Bar", "You Enter the Bar, a few people are sitting down drink their booze.", false);
            //
            Location BanditEncampment = new Location(Location_ID_Bandit_Encampment, "Bandit Encampment", "Bandits stand gaurd, The wall that surrounds them makes it only one way in one way out.", false);
            //
            Location Pasture = new Location(Location_ID_Pasture, "Pasture", "You stand in the pasture, where your camps lays.", false);
            Location p1 = new Location(Location_ID_Pasture, "Pasture", "The open green field stands before you.", false);
            Location p2 = new Location(Location_ID_Pasture, "Pasture", "The open green field stands before you.", false);
            //
            Location Cave = new Location(Location_ID_Cave, "Cave Entrance", "There seems to be a cave wonders of what could be in the bellows of it.", false);
            //
            Cave.NewInstanceOfMonsterLivingHere();
            Cave.AddMonster(Monster_ID_2BearsHighFiving, 20);
            Cave.MonsterAppearanceChance = 80;
            Location DeepCave = new Location(Location_ID_DeepCave, "Deep Cave", "The further you go the darker it gets.", false);
            //
            Location Beach = new Location(Location_ID_Beach, "Beach", "Pearly white sand on a sunny day, to bad theres creatures here.", false);
            //
            Location Water = new Location(Location_ID_Water, "Water", "The water is cool and deep, if you enter it might just rip you ashreds.", false);
            //
            
            Camp.LocationToNorth = Pasture;

            Pasture.LocationToEast = p1;
            Pasture.LocationToWest = p2; //OG LINE
            Pasture.LocationToNorth = Forest;
            Pasture.LocationToSouth = Camp;

            p2.LocationToEast = Pasture; //0,1 TOP ROW
            p2.LocationToNorth = f2;

            p1.LocationToWest = Pasture;
            p1.LocationToNorth = f1; //0,-1 BOTTOM ROW
            //FOREST AREA
            Forest.LocationToNorth = f01; //1,0 OG LINE
            Forest.LocationToEast = f1;
            Forest.LocationToWest = f2;
            Forest.LocationToSouth = Pasture;

            f2.LocationToEast = Forest;//1,1 // top
            f2.LocationToNorth = f4;
            f2.LocationToSouth = p2;

            f1.LocationToNorth = f3;//1,-1
            f1.LocationToSouth = p1;
            f1.LocationToWest = Forest;

            f01.LocationToNorth = f02;//2,0 OG LINE
            f01.LocationToEast = f3;
            f01.LocationToSouth = Forest;
            f01.LocationToWest = f4;

            f4.LocationToNorth = f6;//2,1
            f4.LocationToEast = f01;
            f4.LocationToSouth = f2;

            f3.LocationToNorth = f5;//2.-1
            f3.LocationToWest = f01;
            f3.LocationToSouth = f1;
            f3.LocationToEast = Cave;

            f02.LocationToNorth = f03; //3,0
            f02.LocationToEast = f5;
            f02.LocationToWest = f6;
            f02.LocationToSouth = f01;

            f6.LocationToNorth = f8; //3,1
            f6.LocationToWest = f6a;
            f6.LocationToSouth = f4;
            f6.LocationToEast = f02;

            f5.LocationToNorth = f7; //3,-1
            f5.LocationToSouth = f3;
            f5.LocationToWest = f02;

            f6a.LocationToNorth = Town;//3,2
            f6a.LocationToEast = f6;

            f03.LocationToEast = f7; //4,0
            f03.LocationToSouth = f02;
            f03.LocationToWest = f8;

            f7.LocationToWest = f03; //4,-1
            f7.LocationToSouth = f5;

            f8.LocationToWest = Town; //4,1
            f8.LocationToEast = f03;
            f8.LocationToNorth = f10;
            f8.LocationToSouth = f6;

            f10.LocationToSouth = f8;//5,1
            f10.LocationToWest = f10a;

            f10a.LocationToEast = f10;//5,2
            f10a.LocationToSouth = Town;
            //Town
            Town.LocationToSouth = f6a; //4,2 OR ENTRANCE OF TOWN
            Town.LocationToNorth = f10a;
            Town.LocationToEast = f8;
            Town.LocationToWest = t8a;

            t8a.LocationToWest = t8b;//4,3 
            t8a.LocationToEast = Town;
            t8a.LocationToSouth = Jail; // Jailhouse at 3,3

            t8b.LocationToEast = t8a; // 4,4
            t8b.LocationToNorth = Bar; //Bar
            t8b.LocationToSouth = GuildHall; //Guild Hall
            t8b.LocationToWest = t8c;

            t8c.LocationToEast = t8b;//4,5
            t8c.LocationToSouth = Med;//Med
            t8c.LocationToNorth = Shop;//Shop

            Med.LocationToNorth = t8c; //Med 3,5

            Shop.LocationToSouth = t8c; //Shop 5,5

            GuildHall.LocationToNorth = t8b; //Guild Hall 3,4

            Bar.LocationToSouth = t8b; //Bar 5,4

            Jail.LocationToNorth = t8a; //Jail 3,3
            
            //Cave
            Cave.LocationToWest = f3;
            //BREAKER
            _locations.Add(Camp);
            _locations.Add(Town);
            _locations.Add(Forest);
            _locations.Add(Cave);
            _locations.Add(Pasture);
            _locations.Add(Shop);
        }
        public static _items ItemByID(int id)
        {
            return _items.SingleOrDefault(x => x.ID == id);
        }
        public static Monster MonsterByID(int id)
        {
            return _monsters.SingleOrDefault(x => x.ID == id);
        }
        public static Quest QuestByID (int id)
        {
            return _quests.SingleOrDefault(x => x.ID == id);
        }
        public static Location LocationByID(int id)
        {
            return _locations.SingleOrDefault(x => x.ID == id);
        }
    }
}
