using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BfK_S_ApiProjekt
{
    public class SQLiteManager
    {

        public static SQLiteConnection? SQLiteConnector;

        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;

            sqlite_conn = new SQLiteConnection("Data Source=flashcard_db.db; ");

            // Open the connection:
            try
            {
                sqlite_conn.Open();
                Debug.WriteLine("DB-Connection opend");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DB-Connection Error:" + ex.Message);
            }
            return sqlite_conn;
        }

        public static void SetupTables()
        {
            ExecuteDbCommand("CREATE TABLE IF NOT EXISTS Flashcard(" +
                "FlashcardId INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Front VARCHAR(100)," +
                "Back VARCHAR(100)" +
                ");");
        }

        public static void SaveToDb(List<Flashcard> flashcards)
        {
            foreach(var flashcard in flashcards)
            {
                ExecuteDbCommand($"INSERT INTO Flashcard(Front, Back) VALUES('{flashcard.textFront}', '{flashcard.textBack}'); ");
            }
            Debug.WriteLine($"{flashcards.Count.ToString()} saved to DB");
        }

        public static void ExecuteDbCommand(string command)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = SQLiteConnector.CreateCommand();
            sqlite_cmd.CommandText = command;
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
