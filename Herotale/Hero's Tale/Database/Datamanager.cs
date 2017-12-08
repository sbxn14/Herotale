using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Herotale.Contexts;
using Herotale.Models;

namespace Herotale.Database
{
    public static class Datamanager
    {
        public static List<Character> CharList;
        public static List<Item> ItemList;
        public static List<Inventory> InvenList;
        public static List<Class> ClassList;
        public static List<Race> RaceList;
        public static List<Checkpoint> CPList;
        public static List<Account> AccList;
        public static List<Segment> SegmentList;

        public static void Init()
        {
            SegmentList = DB.RunQuery(new Segment());
			CPList = GetCps();
            AccList = DB.RunQuery(new Account());
            ItemList = GetItems();
            ClassList = DB.RunQuery(new Class());
            RaceList = DB.RunQuery(new Race());
            InvenList = GetInventories();
            CharList = DB.RunQuery(new Character());
        }

        public static List<Inventory> GetInventories()
        {
            return DB.RunQuery(new Inventory());
        }

        public static List<Item> GetItems()
        {
            return DB.RunQuery(new Item());
        }

        public static List<Checkpoint> GetCps()
        {
            return DB.RunQuery(new Checkpoint());
        }

        public static Item GetEmptyItem()
        {
            SqlDataReader reader = null;
            Item i = new Item();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Items WHERE Name=@name", conn);
            cmd.Parameters.AddWithValue("@name", "Empty");
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    i.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    i.Name = reader.GetString(reader.GetOrdinal("Name"));
                    i.AttackBonus = reader.GetInt32(reader.GetOrdinal("Attack Power Bonus"));
                    i.DefenseBonus = reader.GetInt32(reader.GetOrdinal("Defense Bonus"));
                    i.SpeedBonus = reader.GetInt32(reader.GetOrdinal("Speed Bonus"));

                    conn.Close();
                    conn.Dispose();

                    return i;
                }
            }
            conn.Close();
            conn.Dispose();
            return null;
        }
    }
}