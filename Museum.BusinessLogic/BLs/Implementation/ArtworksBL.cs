using AutoMapper;
using Museum.BusinessLogic.BLs.Contracts;
using Museum.BusinessLogic.BusinessObjects;
using Museum.BusinessLogic.BusinessObjects.MapperProfiles;
using Museum.DataAccess.Entities;
using Museum.DataAccess.Repositories.Contracts;

namespace Museum.BusinessLogic.BLs.Implementation
{
    public class ArtworksBL : IArtworksBL
    {
        private readonly IArtworksRepo _artworksRepo;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public ArtworksBL(IArtworksRepo artworksRepo)
        {
            _artworksRepo = artworksRepo;
            _mapperConfiguration = new MapperConfiguration(m =>
            {
                m.AddProfile<ArtworksBOProfile>();
                m.AddProfile<ArtworksRepoProfile>();
            });
            _mapper = _mapperConfiguration.CreateMapper();
        }

        public async Task<List<ArtworkBO>> GetAll()
        {
            List<Artwork> artworks = await _artworksRepo.GetAll();
            List<ArtworkBO> artworksBO = _mapper.Map<List<ArtworkBO>>(artworks);
            return artworksBO;
        }

        public async Task<ArtworkBO> Get(int id)
        {
            Artwork artwork = await _artworksRepo.Get(id);
            ArtworkBO artworkBO = _mapper.Map<ArtworkBO>(artwork);
            return artworkBO;
        }

        public async Task<int> Add(ArtworkBO artworkBO)
        {
            Artwork artwork = _mapper.Map<Artwork>(artworkBO);
            int id = await _artworksRepo.Add(artwork);
            return id;
        }

        public async Task Update(ArtworkBO artworkBO)
        {
            Artwork artwork = _mapper.Map<Artwork>(artworkBO);
            await _artworksRepo.Update(artwork);
        }

        public async Task Delete(int id)
        {
            await _artworksRepo.Delete(id);
        }
    }
}
