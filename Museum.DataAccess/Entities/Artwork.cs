
namespace Museum.DataAccess.Entities
{
    public class Artwork
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public float Price { get; set; }
        public float Rating { get; set; }
    }
}
