using NUnit.Framework;
using System.Collections.Generic;

namespace VirtualArtGallery.Tests
{
    [TestFixture]
    public class VirtualArtGalleryServiceTests
    {
        private VirtualArtGalleryService service;

        [SetUp]
        public void Setup()
        {
            
            service = new VirtualArtGalleryService();
        }

      
        [Test]
        public void CreateGallery_ShouldReturnTrue_WhenGalleryIsAdded()
        {
           
            var gallery = new Gallery
            {
                GalleryID = 1,  // You can skip setting this if it's auto-incremented
                Name = "Modern Art Gallery",
                Description = "A gallery showcasing modern artworks",
                Location = "New York"
            };

            // Act
            var result = service.AddGallery(gallery);

            // Assert
            Assert.That(result, Is.True, "Gallery should be added successfully.");
        }

        // Test: Updating gallery information
        [Test]
        public void UpdateGallery_ShouldReturnTrue_WhenGalleryExists()
        {
            // Arrange
            var gallery = new Gallery
            {
                GalleryID = 1,  // Existing gallery ID
                Name = "Updated Art Gallery",
                Description = "Updated description of the gallery",
                Location = "San Francisco"
            };

            // Act
            var result = service.UpdateGallery(gallery);

            // Assert
            Assert.That(result, Is.True, "Gallery information should be updated successfully.");
        }

        // Test: Removing a gallery
        [Test]
        public void RemoveGallery_ShouldReturnTrue_WhenGalleryExists()
        {
            // Arrange
            int galleryID = 1;  // Existing gallery ID

            // Act
            var result = service.RemoveGallery(galleryID);

            // Assert
            Assert.That(result, Is.True, "Gallery should be removed successfully.");
        }

        // Test: Searching for galleries
        [Test]
        public void SearchGalleries_ShouldReturnListOfGalleries_WhenGalleriesMatchKeyword()
        {
            // Arrange
            string keyword = "Modern";

            // Act
            var galleries = service.SearchGalleries(keyword);

            // Assert
            Assert.That(galleries, Is.Not.Null, "Search should return results.");
            Assert.That(galleries.Count, Is.GreaterThan(0), "Search should return at least one gallery.");
        }
    }

    // Assume you have a class like this in your actual project
    public class VirtualArtGalleryService
    {
        public bool AddGallery(Gallery gallery)
        {
            // Implement logic to add gallery to the system
            return true; // Just for demonstration
        }

        public bool UpdateGallery(Gallery gallery)
        {
            // Implement logic to update gallery
            return true; // Just for demonstration
        }

        public bool RemoveGallery(int galleryID)
        {
            // Implement logic to remove gallery from the system
            return true; // Just for demonstration
        }

        public List<Gallery> SearchGalleries(string keyword)
        {
            // Implement logic to search for galleries by keyword
            return new List<Gallery>
            {
                new Gallery { GalleryID = 1, Name = "Modern Art Gallery", Location = "New York" }
            };
        }
    }

    // Gallery class definition (simplified)
    public class Gallery
    {
        public int GalleryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
