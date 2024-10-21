using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Entities;
using MyExceptions;

namespace Dao
{
    public class VirtualArtGalleryService : IVirtualArtGallery
    {
        private static SqlConnection connection;

        public VirtualArtGalleryService()
        {
            connection = DBConnection.GetConnection();
        }

        public bool AddArtwork(Artwork artwork)
        {
            try
            {
                string query = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL, ArtistID) VALUES (@Title, @Description, @CreationDate, @Medium, @ImageURL, @ArtistID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", artwork.Title);
                    command.Parameters.AddWithValue("@Description", artwork.Description);
                    command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    command.Parameters.AddWithValue("@Medium", artwork.Medium);
                    command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    command.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            try
            {
                string query = "UPDATE Artwork SET Title=@Title, Description=@Description, CreationDate=@CreationDate, Medium=@Medium, ImageURL=@ImageURL WHERE ArtworkID=@ArtworkID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", artwork.Title);
                    command.Parameters.AddWithValue("@Description", artwork.Description);
                    command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    command.Parameters.AddWithValue("@Medium", artwork.Medium);
                    command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    command.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool RemoveArtwork(int artworkID)
        {
            try
            {
                string query = "DELETE FROM Artwork WHERE ArtworkID=@ArtworkID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ArtworkID", artworkID);
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Artwork GetArtworkById(int artworkID)
        {
            try
            {
                string query = "SELECT * FROM Artwork WHERE ArtworkID=@ArtworkID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ArtworkID", artworkID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Artwork artwork = new Artwork
                            {
                                ArtworkID = (int)reader["ArtworkID"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                CreationDate = (DateTime)reader["CreationDate"],
                                Medium = reader["Medium"].ToString(),
                                ImageURL = reader["ImageURL"].ToString(),
                                ArtistID = (int)reader["ArtistID"]
                            };
                            return artwork;
                        }
                        else
                        {
                            throw new ArtWorkNotFoundException($"Artwork with ID {artworkID} not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            try
            {
                string query = "SELECT * FROM Artwork WHERE Title LIKE @Keyword OR Description LIKE @Keyword";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Artwork> artworks = new List<Artwork>();

                        while (reader.Read())
                        {
                            Artwork artwork = new Artwork
                            {
                                ArtworkID = (int)reader["ArtworkID"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                CreationDate = (DateTime)reader["CreationDate"],
                                Medium = reader["Medium"].ToString(),
                                ImageURL = reader["ImageURL"].ToString(),
                                ArtistID = (int)reader["ArtistID"]
                            };
                            artworks.Add(artwork);
                        }
                        return artworks;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // User Favorites

        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            try
            {
                string query = "INSERT INTO FavoriteArtworks (UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@ArtworkID", artworkId);

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            try
            {
                string query = "DELETE FROM FavoriteArtworks WHERE UserID=@UserID AND ArtworkID=@ArtworkID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@ArtworkID", artworkId);

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            try
            {
                // Check if the user exists first
                string userCheckQuery = "SELECT * FROM [User] WHERE UserID=@UserID";
                using (SqlCommand command = new SqlCommand(userCheckQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new UserNotFoundException($"User with ID {userId} not found.");
                        }
                    }
                }

                // If user exists, fetch their favorite artworks
                string query = "SELECT a.* FROM Artwork a INNER JOIN FavoriteArtworks ufa ON a.ArtworkID = ufa.ArtworkID WHERE ufa.UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Artwork> favoriteArtworks = new List<Artwork>();

                        while (reader.Read())
                        {
                            Artwork artwork = new Artwork
                            {
                                ArtworkID = (int)reader["ArtworkID"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                CreationDate = (DateTime)reader["CreationDate"],
                                Medium = reader["Medium"].ToString(),
                                ImageURL = reader["ImageURL"].ToString(),
                                ArtistID = (int)reader["ArtistID"]
                            };
                            favoriteArtworks.Add(artwork);
                        }
                        return favoriteArtworks;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
