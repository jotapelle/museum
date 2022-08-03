using Museum.BusinessLogic.BusinessObjects;

namespace Museum.BusinessLogic.BLs.Contracts
{
    public interface IArtworksBL
    {
        Task<List<ArtworkBO>> GetAll();
        Task<ArtworkBO> Get(int id);
        Task<int> Add(ArtworkBO artworkBO);
        Task Update(ArtworkBO artworkBO);
        Task Delete(int id);
    }
}