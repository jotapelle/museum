using System.Text.Json.Serialization;

namespace Museum.Api.ViewModels
{
    public class ArtworkVM
    {
        public virtual int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public float Price { get; set; }
        public float Rating { get; set; }
    }
}