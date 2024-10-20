using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using entity;

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
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", artwork.Title);
                command.Parameters.AddWithValue("@Description", artwork.Description);
                command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                command.Parameters.AddWithValue("@Medium", artwork.Medium);
                command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                command.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                int result = command.ExecuteNonQuery();
                return result > 0;
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
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", artwork.Title);
                command.Parameters.AddWithValue("@Description", artwork.Description);
                command.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                command.Parameters.AddWithValue("@Medium", artwork.Medium);
                command.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                command.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);

                int result = command.ExecuteNonQuery();
                return result > 0;
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
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ArtworkID", artworkID);

                int result = command.ExecuteNonQuery();
                return result > 0;
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
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ArtworkID", artworkID);
                SqlDataReader reader = command.ExecuteReader();

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
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataReader reader = command.ExecuteReader();

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
                string query = "INSERT INTO User_Favorite_Artwork (UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
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
                string query = "DELETE FROM User_Favorite_Artwork WHERE UserID=@UserID AND ArtworkID=@ArtworkID";
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

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            try
            {
                string query = "SELECT * FROM User WHERE UserID=@UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    throw new UserNotFoundException($"User with ID {userId} not found.");
                }

               
                reader.Close();
                query = "SELECT a.* FROM Artwork a INNER JOIN User_Favorite_Artwork ufa ON a.ArtworkID = ufa.ArtworkID WHERE ufa.UserID = @UserID";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                reader = command.ExecuteReader();

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
