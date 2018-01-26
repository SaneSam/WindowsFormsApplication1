using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Engine
{
    public static class PlayerDataMapper
    {
        private static readonly string _connectionString = "Data Source=(local);Initial Catalog=SuperAdventure;Integrated Security=True";

        public static Player CreateFromDataBase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    Player player;

                    using (SqlCommand savedGameCommand = connection.CreateCommand())
                    {
                        savedGameCommand.CommandType = CommandType.Text;
                        savedGameCommand.CommandText = "SELECT TOP 1 * FROM SavedGame";

                        SqlDataReader reader = savedGameCommand.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            return null;
                        }
                        reader.Read();

                        int HP = (int)reader["HP"];
                        int MaxHP = (int)reader["MaxHP"];
                        int gold = (int)reader["Gold"];
                        int EXP = (int)reader["EXP"];
                        int CurrentLocationID = (int)reader["CurrentLocationID"];

                        player = Player.CreatePlayerfromDatabase(HP, MaxHP, gold, EXP, CurrentLocationID);
                    }
                    using (SqlCommand questCommand = connection.CreateCommand())
                    {
                        questCommand.CommandType = CommandType.Text;
                        questCommand.CommandText = "SELECT * FROM Quest";

                        SqlDataReader reader = questCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int questID = (int)reader["QuestID"];
                                bool isCompleted = (bool)reader["IsCompleted"];

                                PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(questID));
                                playerQuest.IsCompleted = isCompleted;

                                player.Quest.Add(playerQuest);
                            }
                        }
                    }
                    using (SqlCommand inventoryCommand = connection.CreateCommand())
                    {
                        inventoryCommand.CommandType = CommandType.Text;
                        inventoryCommand.CommandText = "SELECT * FROM Inventory";

                        SqlDataReader reader = inventoryCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int inventoryItemID = (int)reader["InventoryItemID"];
                                int quantity = (int)reader["Quantity"];
                                player.additemtoinventor(World.ItemByID(inventoryItemID), quantity);
                            }
                        }
                    }
                    return player;
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return null;
        }

        public static void SaveToDatabase(Player player)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand existingrowcountcommand = connection.CreateCommand())
                    {
                        existingrowcountcommand.CommandType = CommandType.Text;
                        existingrowcountcommand.CommandText = "SLECT count(*) FROM SavedGame";

                        int existingRowCount = (int)existingrowcountcommand.ExecuteScalar();

                        if (existingRowCount == 0)
                        {
                            using (SqlCommand insertSavedGame = connection.CreateCommand())
                            {
                                insertSavedGame.CommandType = CommandType.Text;
                                insertSavedGame.CommandText =
                                    "INSERT INTO SavedGame " +
                                    "(CurrentHit points, MaximumHit points, Gold, Experience points, CurrentLocationID) " +
                                    "VALUES " +
                                    "(@CurrentHit points, @MaximumHit points, @Gold, @Experience points, @CurrentLocationID)";

                                insertSavedGame.Parameters.Add("@HP", SqlDbType.Int);
                                insertSavedGame.Parameters["@HP"].Value = player.HP;
                                insertSavedGame.Parameters.Add("@MaxHP", SqlDbType.Int);
                                insertSavedGame.Parameters["@MaxHP"].Value = player.MaxHP;
                                insertSavedGame.Parameters.Add("@Gold", SqlDbType.Int);
                                insertSavedGame.Parameters["@Gold"].Value = player.Gold;
                                insertSavedGame.Parameters.Add("@EXP", SqlDbType.Int);
                                insertSavedGame.Parameters["@EXP"].Value = player.EXP;
                                insertSavedGame.Parameters.Add("@CurrentLocationID", SqlDbType.Int);
                                insertSavedGame.Parameters["@CurrentLocationID"].Value = player.CurrentLocation.ID;

                                insertSavedGame.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand updateSavedGame = connection.CreateCommand())
                            {
                                updateSavedGame.CommandType = CommandType.Text;
                                updateSavedGame.CommandText =
                                    "UPDATE SavedGame " +
                                    "SET CurrentHit points = @CurrentHit points, " +
                                    "MaximumHit points = @MaximumHit points, " +
                                    "Gold = @Gold, " +
                                    "Experience points = @Experience points, " +
                                    "CurrentLocationID = @CurrentLocationID";

                                updateSavedGame.Parameters.Add("@HP", SqlDbType.Int);
                                updateSavedGame.Parameters["@CurrentHit points"].Value = player.HP;
                                updateSavedGame.Parameters.Add("@MaxHP", SqlDbType.Int);
                                updateSavedGame.Parameters["@MaxHP"].Value = player.MaxHP;
                                updateSavedGame.Parameters.Add("@Gold", SqlDbType.Int);
                                updateSavedGame.Parameters["@Gold"].Value = player.Gold;
                                updateSavedGame.Parameters.Add("@EXP", SqlDbType.Int);
                                updateSavedGame.Parameters["@EXP"].Value = player.EXP;
                                updateSavedGame.Parameters.Add("@CurrentLocationID", SqlDbType.Int);
                                updateSavedGame.Parameters["@CurrentLocationID"].Value = player.CurrentLocation.ID;

                                updateSavedGame.ExecuteNonQuery();
                            }
                        }
                    }
                    using (SqlCommand deleteQuestCommand = connection.CreateCommand())
                    {
                        deleteQuestCommand.CommandType = CommandType.Text;
                        deleteQuestCommand.CommandText = "DELETE FROM Quest";

                        deleteQuestCommand.ExecuteNonQuery();
                    }

                    foreach (PlayerQuest playerQuest in player.Quest)
                    {
                        using (SqlCommand insertQuestCommand = connection.CreateCommand())
                        {
                            insertQuestCommand.CommandType = CommandType.Text;
                            insertQuestCommand.CommandText = "INSERT INTO Quest (QuestID, IsCompleted) VALUES (@QuestID, @IsCompleted)";

                            insertQuestCommand.Parameters.Add("@QuestID", SqlDbType.Int);
                            insertQuestCommand.Parameters["@QuestID"].Value = playerQuest.Details.ID;
                            insertQuestCommand.Parameters.Add("@IsCompleted", SqlDbType.Bit);
                            insertQuestCommand.Parameters["@IsCompleted"].Value = playerQuest.IsCompleted;

                            insertQuestCommand.ExecuteNonQuery();
                        }
                    }
                    using (SqlCommand deleteInventoryCommand = connection.CreateCommand())
                    {
                        deleteInventoryCommand.CommandType = CommandType.Text;
                        deleteInventoryCommand.CommandText = "DELETE FROM Inventory";

                        deleteInventoryCommand.ExecuteNonQuery();
                    }
                    foreach (Inventory inventoryItem in player.Inventory)
                    {
                        using (SqlCommand insertInventoryCommand = connection.CreateCommand())
                        {
                            insertInventoryCommand.CommandType = CommandType.Text;
                            insertInventoryCommand.CommandText = "INSERT INTO Inventory (InventoryItemID, Quantity) VALUES (@InventoryItemID, @Quantity)";

                            insertInventoryCommand.Parameters.Add("@InventoryItemID", SqlDbType.Int);
                            insertInventoryCommand.Parameters["@InventoryItemID"].Value = inventoryItem.Details.ID;
                            insertInventoryCommand.Parameters.Add("@Quantity", SqlDbType.Int);
                            insertInventoryCommand.Parameters["@Quantity"].Value = inventoryItem.Quantity;

                            insertInventoryCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
        }
    }
}
