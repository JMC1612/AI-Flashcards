using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace BfK_S_ApiProjekt
{
    public class SQLiteManager
    {

        public static SQLiteConnection? SQLiteConnector;
        private static string connectionString = $"Data Source={App.DbFileName}; ";
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;

            sqlite_conn = new SQLiteConnection(connectionString);

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
            Debug.WriteLine("Flashcard table created.");
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
            try
            {
                // Beispiel: Prüfen, ob die Verbindung offen ist
                if (SQLiteConnector != null && SQLiteConnector.State == ConnectionState.Open)
                {
                    Debug.WriteLine("DB-Connection is open, executing command.");
                }
            }
            catch (ObjectDisposedException)
            {
                SQLiteConnector = CreateConnection();
            }

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = SQLiteConnector.CreateCommand();
            sqlite_cmd.CommandText = command;
            sqlite_cmd.ExecuteNonQuery();
        }

        public static List<Flashcard> ReadAllFlashcardsFromDb()
        {
            List<Flashcard> flashcards = new List<Flashcard>();

            using (SQLiteConnector)
            {
                string query = "SELECT Front, Back FROM Flashcard";

                using (SQLiteCommand command = new SQLiteCommand(query, SQLiteConnector))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flashcard flashcard = new Flashcard(reader.GetString("Front"), reader.GetString("Back"));
                            flashcards.Add(flashcard);
                        }
                    }
                }
            }

            Debug.WriteLine($"Retrieved {flashcards.Count} flashcards from database");
            return flashcards;
        }
    }
}
