using System.Text.Json.Serialization;

namespace Museum.Api.ViewModels
{
    public class ArtworkExtVM : ArtworkVM
    {
        [JsonIgnore]
        public override int Id { get; set; }
    }
}