using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI.Repositories
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {

        public JournalRepository(string connectionString) : base(connectionString) {  }

        public void Insert(Journal jEntry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                        OUTPUT INSERTED.Id
                                        VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue("@title", jEntry.Title);
                    cmd.Parameters.AddWithValue("@content", jEntry.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", jEntry.CreateDateTime);
                    int id = (int)cmd.ExecuteScalar();

                    jEntry.Id = id;
                }
            }
        }

        public Journal Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Title, Content FROM Journal Where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Journal entry = null;

                    if (reader.Read())
                    {
                        entry = new Journal
                        {
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content"))
                        };
                    }

                    reader.Close();

                    return entry;
                }
            }
        }



        public List<Journal> GetAll()
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

        public void Update(Journal entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal
                                        SET Title = @title,
                                            Content = @content
                                        Where ID = @id";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@id", entry.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Journal WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }




    }
}
