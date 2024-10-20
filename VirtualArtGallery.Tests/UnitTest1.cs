using NUnit.Framework;
using Dao;
using Entities;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class VirtualArtGalleryServiceTests
    {
        private VirtualArtGalleryService _service;

        [SetUp]
        public void Setup()
        {
            _service = new VirtualArtGalleryService();
        }

        [Test]
        public void AddArtwork_ShouldReturnTrue_WhenArtworkIsValid()
        {
            
            var artwork = new Artwork
            {
                Title = "Sunset",
                Description = "A beautiful sunset.",
                CreationDate = new DateTime(2024, 01, 01),
                Medium = "Oil Painting",
                ImageURL = "http://example.com/sunset.jpg",
                ArtistID = 1
            };

          
            var result = _service.AddArtwork(artwork);

          
            Assert.That(result, "Artwork should be added successfully.");
        }

        [Test]
        public void UpdateArtwork_ShouldReturnTrue_WhenArtworkExists()
        {
         
            var artwork = new Artwork
            {
                ArtworkID = 5,
                Title = "Sunset",
                Description = "An updated description.",
                CreationDate = new DateTime(2024, 01, 01),
                Medium = "Oil Painting",
                ImageURL = "http://example.com/sunset_updated.jpg"
            };

          
            var result = _service.UpdateArtwork(artwork);

           
            Assert.That(result, "Artwork should be updated successfully.");
        }

        [Test]
        public void RemoveArtwork_ShouldReturnTrue_WhenArtworkExists()
        {
          
            int artworkID = 8;

           
            var result = _service.RemoveArtwork(artworkID);

           
            Assert.That(result, "Artwork should be removed successfully.");
        }

        [Test]
        public void SearchArtworks_ShouldReturnResults_WhenKeywordIsValid()
        {
        
            var artworks = _service.SearchArtworks("sunset");

            Assert.That(artworks, Is.Not.Null, "Search should return results.");
            Assert.That(artworks.Count > 0, "At least one artwork should be returned.");
        }
    }
}
