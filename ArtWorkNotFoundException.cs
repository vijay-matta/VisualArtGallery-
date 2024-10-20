using System;

namespace MyExceptions
{
    public class ArtWorkNotFoundException : Exception
    {
        public ArtWorkNotFoundException() : base("Artwork not found with the given ID.") { }

        public ArtWorkNotFoundException(string message) : base(message) { }

        public ArtWorkNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
