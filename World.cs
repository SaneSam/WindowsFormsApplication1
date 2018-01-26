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
        public const int Item_ID_Sword = 1; //130gp
        public const int Item_ID_Bow = 2; //110gp
        public const int Item_ID_Staff = 3; //120gp
        public const int Item_ID_hedgeapple = 4; //6gp
        public const int Item_ID_arcobot_Fang = 5; //4gp
        public const int Item_ID_Chunks_of_leather = 6; //6gp
        public const int Item_ID_GODSWORD = 9999; //beta weapon

        public const int Monster_ID_NOTHING = -1;
        public const int Monster_ID_Arcobot = 1;
        public const int Monster_ID_Bandit = 2;
        public const int Monster_ID_Man_Eater = 3;
        public const int Monster_ID_Ent = 4;
        public const int Monster_ID_2BearsHighFiving = 5;

        public const int Quest_ID_Clear_Kill_5_arcobot = 1;
        public const int Quest_ID_Clear_retreive_5_Chucks_of_lether = 2;

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
            _items.Add(new Weapon(Item_ID_Sword, "Damaged Captain's Sword", "Damaged Captain's Swords", 5, 10,130));
            _items.Add(new Weapon(Item_ID_Bow, "Old Bow", "Old Bows", 3, 12,110));
            _items.Add(new Weapon(Item_ID_Staff, "Rotten staff", "Rotten staffs", 4, 9,120));
            _items.Add(new Weapon(Item_ID_GODSWORD, "GOD SWORD", "GOD SWORDS", 9998, 9999,-1));

            _items.Add(new Healing(Item_ID_hedgeapple, "Hedge apple", "Hedge apples",9999,6)); //CHANGE LATER ONLY FOR BETA IS 9999
            _items.Add(new _items(Item_ID_arcobot_Fang, "Arcobot's Fang", "Arcobot's Fangs",4));
            _items.Add(new _items(Item_ID_Chunks_of_leather, "Chunk of Leather", "chunks of leather",6));

        }

        private static void PopulateMonsters()
        {
            Monster AcroBot = new Monster(Monster_ID_Arcobot,"Arcobot", 12,5,2,50,50,999,4,8);
            AcroBot.LootTable.Add(new LootItem(ItemByID(Item_ID_arcobot_Fang),40,false));

            Monster Bandit = new Monster(Monster_ID_Bandit, "Bandit", 21, 12, 7, 75, 75,999,6,9);
            Bandit.LootTable.Add(new LootItem(ItemByID(Item_ID_Chunks_of_leather), 40, false));

            Monster ManEater = new Monster(Monster_ID_Man_Eater, "Man Eater", 37, 27, 4, 125, 125, 999,12,4);
            ManEater.LootTable.Add(new LootItem(ItemByID(Item_ID_hedgeapple), 100, true));

            Monster Ent = new Monster(Monster_ID_Ent, "Ent", 42, 110, 2, 200, 200, 999,15,12);

            Monster bearshighfiving = new Monster(Monster_ID_2BearsHighFiving, "Bear", 32, 20, 14, 100, 100, 999,1,8);

            _monsters.Add(AcroBot);
            _monsters.Add(Bandit);
            _monsters.Add(ManEater);
            _monsters.Add(Ent);
            _monsters.Add(bearshighfiving);
        }
        private static void PopulateQuest()
        {
            Quest KillArcoBots = new Quest(Quest_ID_Clear_Kill_5_arcobot, "Bot Killer", "Go to the forest and kill 5 arcobots and bring back 3 iron fangs.",40,30);
            KillArcoBots.QuestCompletedItem.Add(new QuestCompletedItem(ItemByID(Item_ID_arcobot_Fang), 3));
            KillArcoBots.rewardItem = ItemByID(Item_ID_hedgeapple);

            Quest Protector = new Quest(Quest_ID_Clear_retreive_5_Chucks_of_lether, "They protect, They Attack, and they are...", "kill off those bandits and retrieve 5 chuncks of leather for us.",70,64);
            Protector.QuestCompletedItem.Add(new QuestCompletedItem(ItemByID(Item_ID_Chunks_of_leather), 5));
            Protector.rewardItem = ItemByID(Item_ID_hedgeapple);//REPLACE rewards with real items later

            _quests.Add(KillArcoBots);
            _quests.Add(Protector);
        }
        private static void PopulateLocation()
        { //odds y<0 evens y>0 
            Location Camp = new Location(Location_ID_Camp, "Camp", "The makeshift place you call home... cozy");
            //
            Location Forest = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Forest.NewInstanceOfMonsterLivingHere();
            Forest.AddMonster(Monster_ID_Bandit, 20);
            Forest.AddMonster(Monster_ID_Arcobot, 20);
            Location f1 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");//0,1
            Location f2 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one."); 
            Location f3 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f4 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f5 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f6 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f7 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f8 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f10 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f10a = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f6a = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f01 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f02 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            Location f03 = new Location(Location_ID_Forst, "Forest", "Tall trees stand in your way, These trees are so big a weagon can fit though one.");
            //
            Location Jungle = new Location(Location_ID_Jungle, "Jungle", "Vines hang from the tree tops, barely little light come from the canopy; don't travel at night here.");
            //
            Location Mountain = new Location(Location_ID_Mountain, "Mountain", "Far pass the town and jungle is mountains it going to take a while to get their; and it looks cold bring a coat.");
            //

            Location Town = new Location(Location_ID_Town, "Lost Town", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");

            Location t8a = new Location(Location_ID_Town, "Lost Town", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");

            Location t8b = new Location(Location_ID_Town, "Lost Town", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");

            Location t8c = new Location(Location_ID_Town, "Lost Town", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");

            Location Jail = new Location(Location_ID_Town, "Jail", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");

            Location GuildHall = new Location(Location_ID_Town, "Guild Hall", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");
            
            GuildHall.QuestAvailableHere = QuestByID(Quest_ID_Clear_retreive_5_Chucks_of_lether);
            GuildHall.QuestAvailableHere = QuestByID(Quest_ID_Clear_Kill_5_arcobot);
            Location Med = new Location(Location_ID_Town, "Clinic", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");

            Location Shop = new Location(Location_ID_LostsSHOP, "Shop", "Welecome to the Shop"); // Change later
            Vendortron9000 Vendy = new Vendortron9000("Vendy");
            Vendy.Additemtoinventory(ItemByID(Item_ID_Chunks_of_leather), 999);
            Vendy.Additemtoinventory(ItemByID(Item_ID_hedgeapple), 999);
            Vendy.Additemtoinventory(ItemByID(Item_ID_Sword), 99);
            Vendy.Additemtoinventory(ItemByID(Item_ID_Staff), 99);
            Vendy.Additemtoinventory(ItemByID(Item_ID_arcobot_Fang), 99);
            Vendy.Additemtoinventory(ItemByID(Item_ID_Bow), 99);
            Shop.Vending = Vendy;

            Location Bar = new Location(Location_ID_Town, "Bar", "Before the Jungle stand a small hut town, peaceful and calm without bandits atleast.");
            //
            Location BanditEncampment = new Location(Location_ID_Bandit_Encampment, "Bandit Encampment", "Bandits stand gaurd, The wall that surrounds them makes it only one way in one way out.");
            //
            Location Pasture = new Location(Location_ID_Pasture, "Pasture", "The open green field stands before you.");
            Location p1 = new Location(Location_ID_Pasture, "Pasture", "The open green field stands before you.");
            Location p2 = new Location(Location_ID_Pasture, "Pasture", "The open green field stands before you.");
            //
            Location Cave = new Location(Location_ID_Cave, "Cave Entrance", "Their seems to be a cave wonders of what could be in the bellows of it.");
            //
            Location DeepCave = new Location(Location_ID_DeepCave, "Deep Cave", "The further you go the darker it gets.");
            //
            Location Beach = new Location(Location_ID_Beach, "Beach", "Pearly white sand on a sunny day, to bad theirs creatures here.");
            //
            Location Water = new Location(Location_ID_Water, "Water", "The water is cool and deep, if you enter it might just rip you ashreds.");
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
