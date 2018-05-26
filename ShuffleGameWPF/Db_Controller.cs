using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleGameWPF
{
    public struct Player
    {
        public Player(string nick, string res)
        {
            NickName = nick;
            Result = res;
        }
        public string NickName { get; set; }
        public string Result { get; set; }
    }

    public static class Db_Controller
    {
        private static SQLiteConnection connection;

        public static bool ContainsPlayer(string player) //checing to contains in database player with specified NickName
        {
            using (connection = new SQLiteConnection("Data Source = dbShuffleGame.db; Version = 3"))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = $"select count(*) from tb_Players where NickName = '{player}'";
                int res = (int)(long)command.ExecuteScalar();
                //connection.Close();
                if (res == 0)
                    return false;
                return true;
            }
        }

        public static void InsertPlayer(string nickName, string result = null) //insert player into database with specified NickName and result of game
        {
            using (connection = new SQLiteConnection("Data Source = dbShuffleGame.db; Version = 3"))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO tb_Players (NickName, Result) values ('{nickName}', '{result}');";
                command.ExecuteNonQuery();
                //connection.Close();
            }
        }

        /// <summary>
        /// Update result in DB for specified NickName
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="result"></param>
        public static void UpdateResult(string nickName, string result)
        {
            using (connection = new SQLiteConnection("Data Source = dbShuffleGame.db; Version = 3"))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = $"update tb_Players set Result = '{result}' where NickName = '{nickName}';";
                command.ExecuteNonQuery();
                //connection.Close();
            }
        }

        /// <summary>
        /// Get all players in list of <Players>
        /// </summary>
        /// <returns></returns>
        public static List<Player> GetPlayers()
        {
            using (connection = new SQLiteConnection("Data Source = dbShuffleGame.db; Version = 3"))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = $"select NickName, Result from tb_Players;";

                List<Player> list = new List<Player>();
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var pl = new Player(reader["NickName"].ToString(), reader["Result"].ToString());
                        list.Add(pl);
                    }
                }
                //connection.Close();
                return list;
            }
            
        }




    }
}
