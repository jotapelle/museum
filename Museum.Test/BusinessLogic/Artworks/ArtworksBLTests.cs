using Moq;
using Museum.BusinessLogic.BusinessObjects;
using Museum.DataAccess.Repositories.Contracts;
using Museum.BusinessLogic.BLs.Implementation;
using Museum.DataAccess.Entities;

namespace Museum.Test.BusinessLogic.Artworks
{
    public class ArtworksBLTests
    {
        private readonly Mock<IArtworksRepo> _artworksRepo;

        public ArtworksBLTests()
        {
            _artworksRepo = new Mock<IArtworksRepo>();
        }

        private static List<Artwork> GetArtworks()
        {
            List<Artwork> artworks = new()
            {
                new Artwork
                {
                    Id = 1,
                    Name = "Picture",
                    Image = "https://fake.com",
                    Rating = 10,
                    Price = 5
                },
                new Artwork
                {
                    Id = 2,
                    Name = "Photo",
                    Image = "https://fake.com",
                    Rating = 3,
                    Price = 5.4f
                }
            };
            return artworks;
        }

        [Fact]
        public async Task GetAll_Artworks()
        {
            _artworksRepo.Setup(x => x.GetAll()).ReturnsAsync(GetArtworks());

            ArtworksBL artworksBL = new ArtworksBL(_artworksRepo.Object);

            List<ArtworkBO> result = await artworksBL.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Get_Artwork()
        {
            Artwork artwork = GetArtworks().First();
            _artworksRepo.Setup(x => x.Get(artwork.Id)).ReturnsAsync(artwork);

            ArtworksBL artworksBL = new ArtworksBL(_artworksRepo.Object);

            ArtworkBO result = await artworksBL.Get(1);

            Assert.NotNull(result);
            Assert.Equal(result.Id, artwork.Id);
            Assert.Equal(result.Name, artwork.Name);
            Assert.Equal(result.Price, artwork.Price);
            Assert.Equal(result.Rating, artwork.Rating);
            Assert.Equal(result.Image, artwork.Image);
        }

        [Fact]
        public async Task Add_Artwork()
        {
            _artworksRepo.Setup(x => x.Add(It.IsAny<Artwork>())).ReturnsAsync(1);

            ArtworksBL artworksBL = new ArtworksBL(_artworksRepo.Object);

            ArtworkBO artworkBO = new ArtworkBO
            {
                Name = "Picture",
                Image = "https://fake.com",
                Rating = 10,
                Price = 5
            };

            int result = await artworksBL.Add(artworkBO);

            _artworksRepo.Verify(x => x.Add(It.IsAny<Artwork>()), Times.Once);
            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Update_Artwork()
        {
            _artworksRepo.Setup(x => x.Update(It.IsAny<Artwork>()));

            ArtworksBL artworksBL = new ArtworksBL(_artworksRepo.Object);

            ArtworkBO artworkBO = new ArtworkBO
            {
                Id = 1,
                Name = "Picture",
                Image = "https://fake.com",
                Rating = 10,
                Price = 5
            };

            await artworksBL.Update(artworkBO);

            _artworksRepo.Verify(x => x.Update(It.IsAny<Artwork>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Artwork()
        {
            int id = 2;
            _artworksRepo.Setup(x => x.Delete(id));

            ArtworksBL artworksBL = new ArtworksBL(_artworksRepo.Object);
            await artworksBL.Delete(id);

            _artworksRepo.Verify(repo => repo.Delete(id), Times.Once);
        }
    }
}