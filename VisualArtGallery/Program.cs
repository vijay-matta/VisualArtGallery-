using System;
using Dao;
using Entities;
using MyExceptions;
using System.Collections.Generic;

namespace main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            // Create an instance of the service class
            VirtualArtGalleryService galleryService = new VirtualArtGalleryService();

            // Loop for user input and method triggering
            while (true)
            {
                Console.WriteLine("\nWelcome To Vitual Art Gallery");
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Add Artwork");
                Console.WriteLine("2. Update Artwork");
                Console.WriteLine("3. Remove Artwork");
                Console.WriteLine("4. Get Artwork by ID");
                Console.WriteLine("5. Search Artworks by keyword");
                Console.WriteLine("6. Add Artwork to Favorites");
                Console.WriteLine("7. Remove Artwork from Favorites");
                Console.WriteLine("8. Get User's Favorite Artworks");
                Console.WriteLine("9. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Add Artwork
                        try
                        {
                           
                            Console.WriteLine("Enter Artwork Title:");
                            string title = Console.ReadLine();

                            Console.WriteLine("Enter Description:");
                            string description = Console.ReadLine();

                            Console.WriteLine("Enter Creation Date (YYYY-MM-DD):");
                            DateTime creationDate = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Enter Medium:");
                            string medium = Console.ReadLine();

                            Console.WriteLine("Enter Image URL:");
                            string imageUrl = Console.ReadLine();

                            Console.WriteLine("Enter Artist ID:");
                            int artistId = Convert.ToInt32(Console.ReadLine());

                            Artwork newArtwork = new Artwork
                            {
                                Title = title,
                                Description = description,
                                CreationDate = creationDate,
                                Medium = medium,
                                ImageURL = imageUrl,
                                ArtistID = artistId
                            };

                            bool isAdded = galleryService.AddArtwork(newArtwork);
                            Console.WriteLine(isAdded ? "Artwork added successfully." : "Failed to add artwork.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 2:
                        // Update Artwork
                        try
                        {
                            Console.WriteLine("Enter Artwork ID to update:");
                            int updateArtworkId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter new Title:");
                            string newTitle = Console.ReadLine();

                            Console.WriteLine("Enter new Description:");
                            string newDescription = Console.ReadLine();

                            Console.WriteLine("Enter new Creation Date (YYYY-MM-DD):");
                            DateTime newCreationDate = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Enter new Medium:");
                            string newMedium = Console.ReadLine();

                            Console.WriteLine("Enter new Image URL:");
                            string newImageUrl = Console.ReadLine();

                            Console.WriteLine("Enter Artist ID:");
                            int newArtistId = Convert.ToInt32(Console.ReadLine());

                            Artwork updatedArtwork = new Artwork
                            {
                                ArtworkID = updateArtworkId,
                                Title = newTitle,
                                Description = newDescription,
                                CreationDate = newCreationDate,
                                Medium = newMedium,
                                ImageURL = newImageUrl,
                                ArtistID = newArtistId
                            };

                            bool isUpdated = galleryService.UpdateArtwork(updatedArtwork);
                            Console.WriteLine(isUpdated ? "Artwork updated successfully." : "Failed to update artwork.");
                        }
                        catch (ArtWorkNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 3:
                        // Remove Artwork
                        try
                        {
                            Console.WriteLine("Enter Artwork ID to remove:");
                            int removeArtworkId = Convert.ToInt32(Console.ReadLine());

                            bool isRemoved = galleryService.RemoveArtwork(removeArtworkId);
                            Console.WriteLine(isRemoved ? "Artwork removed successfully." : "Failed to remove artwork.");
                        }
                        catch (ArtWorkNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 4:
                        // Get Artwork by ID
                        try
                        {
                            Console.WriteLine("Enter Artwork ID:");
                            int artworkId = Convert.ToInt32(Console.ReadLine());

                            Artwork artwork = galleryService.GetArtworkById(artworkId);
                            if (artwork != null)
                            {
                                Console.WriteLine($"Artwork Found: {artwork.Title}, {artwork.Description}, Created on {artwork.CreationDate}");
                            }
                        }
                        catch (ArtWorkNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 5:
                        // Search Artworks by Keyword
                        try
                        {
                            Console.WriteLine("Enter keyword:");
                            string keyword = Console.ReadLine();

                            List<Artwork> artworks = galleryService.SearchArtworks(keyword);
                            if (artworks.Count > 0)
                            {
                                Console.WriteLine("Artworks found:");
                                foreach (var art in artworks)
                                {
                                    Console.WriteLine($"Title: {art.Title}, Description: {art.Description}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No artworks found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 6:
                        // Add Artwork to Favorites
                        try
                        {
                            Console.WriteLine("Enter User ID:");
                            int userId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter Artwork ID:");
                            int favArtworkId = Convert.ToInt32(Console.ReadLine());

                            bool isAddedToFavorites = galleryService.AddArtworkToFavorite(userId, favArtworkId);
                            Console.WriteLine(isAddedToFavorites ? "Artwork added to favorites." : "Failed to add artwork to favorites.");
                        }
                        catch (UserNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArtWorkNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 7:
                        // Remove Artwork from Favorites
                        try
                        {
                            Console.WriteLine("Enter User ID:");
                            int removeFavUserId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter Artwork ID:");
                            int removeFavArtworkId = Convert.ToInt32(Console.ReadLine());

                            bool isRemovedFromFavorites = galleryService.RemoveArtworkFromFavorite(removeFavUserId, removeFavArtworkId);
                            Console.WriteLine(isRemovedFromFavorites ? "Artwork removed from favorites." : "Failed to remove artwork from favorites.");
                        }
                        catch (UserNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ArtWorkNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 8:
                        // Get User's Favorite Artworks
                        try
                        {
                            Console.WriteLine("Enter User ID:");
                            int favoriteUserId = Convert.ToInt32(Console.ReadLine());

                            List<Artwork> userFavorites = galleryService.GetUserFavoriteArtworks(favoriteUserId);
                            if (userFavorites.Count > 0)
                            {
                                Console.WriteLine("User's favorite artworks:");
                                foreach (var art in userFavorites)
                                {
                                    Console.WriteLine($"Title: {art.Title}, Description: {art.Description}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No favorite artworks found.");
                            }
                        }
                        catch (UserNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case 9:
                        // Exit the loop
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid action.");
                        break;
                }
            }
        }
    }
}
