using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {

        public JournalRepository(string connectionString) : base(connectionString) {  }

        public void Add(Journal jEntry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime
                                        OUTPUT INSERTED.Id
                                        VALUES (@title, @content, @createDateTime";
                    cmd.Parameters.AddWithValue("@title", jEntry.Title);
                    cmd.Parameters.AddWithValue("@content", jEntry.Content);
                    cmd.Parameters.AddWithValue("@publishDateTime", jEntry.CreateDateTime);
                    int id = (int)cmd.ExecuteScalar();

                    jEntry.Id = id;
                }
            }
        }

        public List<Journal> List()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, Content, CreateDateTime" +
                        "               FROM Journal";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Journal> entries = new List<Journal>();

                    while(reader.Read())
                    {
                        Journal entry = new Journal
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };

                        entries.Add(entry);
                    }

                    reader.Close();

                    return entries;
                }
            }
        
        }

        public void Edit(Journal entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal
                                        SET Title = @title,
                                            Content = @content,
                                        Where ID = @id";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@id", entry.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Void 




    }
}
