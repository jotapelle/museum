using Museum.DataAccess.Entities;

namespace Museum.DataAccess.Repositories.Contracts
{
    public interface IArtworksRepo
    {
        Task<List<Artwork>> GetAll();
        Task<Artwork> Get(int id);
        Task<int> Add(Artwork artwork);
        Task Update(Artwork artwork);
        Task Delete(int id);
    }
}