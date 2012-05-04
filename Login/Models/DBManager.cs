using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;


namespace Login.Models
{
    public class DBManager
    {
        private DBManager() { }
        private static DBManager instance = null;

        //private static string ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\dev\\asp\\sopra\\Login\\Login\\App_Data\\Sopra.mdf;Integrated Security=True;User Instance=True";
        private static string ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|Sopra.mdf;Integrated Security=True;User Instance=True";
        private SqlConnection con = new SqlConnection(ConnectionString);


        public static DBManager getInstance()
        {
            if (instance == null)
            {
                instance = new DBManager();
                instance.connect();
            }
            
            return instance;
        }

        /**
         * Verbindung zur Datenbank aufbauen
         */
        private void connect()
        {   
            con.Open();
        }
        
        public SqlDataReader auslesen(string query)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            return cmd.ExecuteReader();
        }

        public int aendern(string query)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            return cmd.ExecuteNonQuery();
        }
    }
}