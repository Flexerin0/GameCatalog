namespace GameCatalog.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Developer { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
