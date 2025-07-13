using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using BfK_S_ApiProjekt.Items;
using System.Data;
using System.Collections.ObjectModel;

namespace BfK_S_ApiProjekt
{
    public class SQLiteManager
    {
        public static string DbFileName = "flashcard_db.db";

        public static SQLiteConnection SQLiteConnector;
        private static string connectionString = $"Data Source={DbFileName};Version=3;";

        public static void Initialize()
        {
            if (SQLiteConnector == null)
            {
                SQLiteConnector = new SQLiteConnection(connectionString);
                try
                {
                    SQLiteConnector.Open();
                    Debug.WriteLine("DB-Connection opened.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("DB-Connection error: " + ex.Message);
                }
            }

            SetupTables();
        }

        public static void SetupTables()
        {
            ExecuteDbCommand(@"CREATE TABLE IF NOT EXISTS Themes (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL
        );");

            ExecuteDbCommand(@"CREATE TABLE IF NOT EXISTS Flashcards (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            FrontText TEXT NOT NULL,
            BackText TEXT NOT NULL,
            ThemeId INTEGER NOT NULL,
            FOREIGN KEY(ThemeId) REFERENCES Themes(Id)
        );");

            Debug.WriteLine("Tables created.");
        }

        public static void ExecuteDbCommand(string command, Dictionary<string, object>? parameters = null)
        {
            try
            {
                if (SQLiteConnector.State != ConnectionState.Open)
                    SQLiteConnector.Open();
            }
            catch (ObjectDisposedException)
            {
                Initialize();
            }

            using var sqlite_cmd = new SQLiteCommand(command, SQLiteConnector);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    sqlite_cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            sqlite_cmd.ExecuteNonQuery();
        }



        public static ObservableCollection<Theme> LoadThemes()
        {
            var themes = new ObservableCollection<Theme>();
            string query = "SELECT Id, Name FROM Themes";

            using var cmd = new SQLiteCommand(query, SQLiteConnector);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                themes.Add(new Theme
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return themes;
        }

        public static void InsertTheme(Theme theme)
        {
            string query = "INSERT INTO Themes (Name) VALUES (@name)";
            var parameters = new Dictionary<string, object>
        {
            { "@name", theme.Name }
        };

            ExecuteDbCommand(query, parameters);
        }

        public static void UpdateTheme(Theme theme)
        {
            string query = "UPDATE Themes SET Name = @name WHERE Id = @id";
            var parameters = new Dictionary<string, object>
        {
            { "@name", theme.Name },
            { "@id", theme.Id }
        };

            ExecuteDbCommand(query, parameters);
        }

        public static void DeleteTheme(int themeId)
        {
            string query = "DELETE FROM Themes WHERE Id = @id";
            var parameters = new Dictionary<string, object>
        {
            { "@id", themeId }
        };

            ExecuteDbCommand(query, parameters);
        }

        public static ObservableCollection<Flashcard> LoadFlashcardsForTheme(int themeId)
        {
            var flashcards = new ObservableCollection<Flashcard>();
            string query = "SELECT ID, FrontText, BackText FROM Flashcards WHERE ThemeId = @themeId";

            using var cmd = new SQLiteCommand(query, SQLiteConnector);
            cmd.Parameters.AddWithValue("@themeId", themeId);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                flashcards.Add(new Flashcard
                {
                    ID = reader.GetInt32(0),
                    FrontText = reader.GetString(1),
                    BackText = reader.GetString(2)
                });
            }

            return flashcards;
        }



        public static void InsertFlashcard(Flashcard flashcard, int themeId)
        {
            string query = "INSERT INTO Flashcards (FrontText, BackText, ThemeId) VALUES (@front, @back, @themeId)";
            var parameters = new Dictionary<string, object>
        {
            { "@front", flashcard.FrontText },
            { "@back", flashcard.BackText },
            { "@themeId", themeId }
        };

            ExecuteDbCommand(query, parameters);
        }

        public static void UpdateFlashcard(Flashcard flashcard)
        {
            string query = "UPDATE Flashcards SET FrontText = @front, BackText = @back WHERE ID = @id";
            var parameters = new Dictionary<string, object>
        {
            { "@front", flashcard.FrontText },
            { "@back", flashcard.BackText },
            { "@id", flashcard.ID }
        };

            ExecuteDbCommand(query, parameters);
        }

        public static void DeleteFlashcard(int flashcardId)
        {
            string query = "DELETE FROM Flashcards WHERE ID = @id";
            var parameters = new Dictionary<string, object>
        {
            { "@id", flashcardId }
        };

            ExecuteDbCommand(query, parameters);
        }
    }

}

