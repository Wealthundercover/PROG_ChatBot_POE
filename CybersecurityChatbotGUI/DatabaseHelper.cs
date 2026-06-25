using System;
using System.Data.SQLite;
using System.IO;

namespace CybersecurityChatbotGUI
{
    public static class DatabaseHelper
    {
        private const string DbFileName = "cybersecurity_db.sqlite";
        private static readonly string ConnectionString = $"Data Source={DbFileName};Version=3;";

        // Initializes the database file and creates the table if it doesn't exist
        public static void InitializeDatabase()
        {
            if (!File.Exists(DbFileName))
            {
                SQLiteConnection.CreateFile(DbFileName);
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS tasks (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        title TEXT NOT NULL,
                        description TEXT NOT NULL,
                        reminder TEXT NULL,
                        is_completed INTEGER DEFAULT 0
                    );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Inserts a new task into the database
        public static void SaveTask(string title, string description, string reminder)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO tasks (title, description, reminder) VALUES (@title, @description, @reminder);";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@reminder", (object)reminder ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}