using Moq;
using Museum.BusinessLogic.BusinessObjects;
using Museum.BusinessLogic.BLs.Contracts;
using Museum.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Museum.Api.ViewModels;

namespace Museum.Test.Api.Artworks
{
    public class ArtworksControllerTests
    {
        private readonly Mock<IArtworksBL> _artworksBL;

        public ArtworksControllerTests()
        {
            _artworksBL = new Mock<IArtworksBL>();
        }

        private static List<ArtworkBO> GetArtworks()
        {
            List<ArtworkBO> artworks = new()
            {
                new ArtworkBO
                {
                    Id = 1,
                    Name = "Picture",
                    Image = "https://fake.com",
                    Rating = 10,
                    Price = 5
                },
                new ArtworkBO
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
        public async Task Get_Artworks()
        {
            _artworksBL.Setup(x => x.GetAll()).ReturnsAsync(GetArtworks());

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);

            IActionResult result = await artworksController.Get();

            OkObjectResult? okResult = result as OkObjectResult;

            List<ArtworkVM> okValue = okResult.Value as List<ArtworkVM>;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(2, okValue.Count());
        }

        [Fact]
        public async Task Get_Artwork()
        {
            ArtworkBO artwork = GetArtworks().First();
            _artworksBL.Setup(x => x.Get(artwork.Id)).ReturnsAsync(artwork);

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);

            IActionResult result = await artworksController.Get(1);

            OkObjectResult? okResult = result as OkObjectResult;

            ArtworkVM okValue = okResult.Value as ArtworkVM;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(okValue.Id, artwork.Id);
            Assert.Equal(okValue.Name, artwork.Name);
            Assert.Equal(okValue.Price, artwork.Price);
            Assert.Equal(okValue.Rating, artwork.Rating);
            Assert.Equal(okValue.Image, artwork.Image);
        }

        [Fact]
        public async Task Add_Artwork()
        {
            _artworksBL.Setup(x => x.Add(It.IsAny<ArtworkBO>())).ReturnsAsync(1);

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);

            ArtworkExtVM artworkExtVM = new ArtworkExtVM
            {
                Name = "Picture",
                Image = "https://fake.com",
                Rating = 10,
                Price = 5
            };

            IActionResult result = await artworksController.Post(artworkExtVM);

            _artworksBL.Verify(x => x.Add(It.IsAny<ArtworkBO>()), Times.Once);

            OkObjectResult? okResult = result as OkObjectResult;

            ArtworkVM okValue = okResult.Value as ArtworkVM;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(okValue.Name, artworkExtVM.Name);
            Assert.Equal(okValue.Price, artworkExtVM.Price);
            Assert.Equal(okValue.Rating, artworkExtVM.Rating);
            Assert.Equal(okValue.Image, artworkExtVM.Image);
        }

        [Fact]
        public async Task Add_Artwork_Empty()
        {
            _artworksBL.Setup(x => x.Add(It.IsAny<ArtworkBO>())).ReturnsAsync(1);

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);

            IActionResult result = await artworksController.Post(null);

            BadRequestObjectResult badRequestObjectResult = result as BadRequestObjectResult;

            Assert.Equal(400, badRequestObjectResult.StatusCode);
        }

        [Fact]
        public async Task Update_Artwork()
        {
            ArtworkBO artworkBO = new ArtworkBO
            {
                Id = 1,
                Name = "Picture",
                Image = "https://fake.com",
                Rating = 10,
                Price = 5
            };

            _artworksBL.Setup(x => x.Get(artworkBO.Id)).ReturnsAsync(artworkBO);
            _artworksBL.Setup(x => x.Update(artworkBO));

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);

            ArtworkVM artworkVM = new ArtworkVM
            {
                Id = 1,
                Name = "Picture",
                Image = "https://fake.com",
                Rating = 10,
                Price = 5
            };

            IActionResult result = await artworksController.Put(artworkVM);

            OkResult? okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Update_Artwork_NoExist()
        {
            ArtworkBO artworkBO = new ArtworkBO
            {
                Id = 1,
                Name = "Picture",
                Image = "https://fake.com",
                Rating = 10,
                Price = 5
            };

            _artworksBL.Setup(x => x.Update(artworkBO));

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);

            ArtworkVM artworkVM = new ArtworkVM
            {
                Id = 1,
                Name = "Picture",
                Image = "https://fake.com",
                Rating = 10,
                Price = 5
            };

            IActionResult result = await artworksController.Put(artworkVM);

            BadRequestObjectResult? badRequestObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(badRequestObjectResult);
            Assert.Equal(400, badRequestObjectResult.StatusCode);
        }

        [Fact]
        public async Task Update_Artwork_Empty()
        {
            _artworksBL.Setup(x => x.Update(null));

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);

            IActionResult result = await artworksController.Put(null);

            BadRequestObjectResult? badRequestObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(badRequestObjectResult);
            Assert.Equal(400, badRequestObjectResult.StatusCode);
        }

        [Fact]
        public async Task Delete_Artwork()
        {
            int id = 1;
            ArtworkBO artwork = GetArtworks().First();
            _artworksBL.Setup(x => x.Get(id)).ReturnsAsync(artwork);
            _artworksBL.Setup(x => x.Delete(id));

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);
            IActionResult result = await artworksController.Delete(id);

            _artworksBL.Verify(repo => repo.Delete(id), Times.Once);

            OkResult? okResult = result as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Delete_Artwork_NoExist()
        {
            int id = 1;
            _artworksBL.Setup(x => x.Delete(id));

            ArtworksController artworksController = new ArtworksController(_artworksBL.Object);
            IActionResult result = await artworksController.Delete(id);

            BadRequestObjectResult? badRequestObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(badRequestObjectResult);
            Assert.Equal(400, badRequestObjectResult.StatusCode);
        }
    }
}