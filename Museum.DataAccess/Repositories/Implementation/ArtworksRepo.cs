using Microsoft.EntityFrameworkCore;
using Museum.DataAccess.Context;
using Museum.DataAccess.Entities;
using Museum.DataAccess.Repositories.Contracts;

namespace Museum.DataAccess.Repositories.Implementation
{
    public class ArtworksRepo : IArtworksRepo
    {
        private readonly MuseumContext _museumContext;

        public ArtworksRepo(MuseumContext museumContext)
        {
            _museumContext = museumContext;
        }

        public async Task<List<Artwork>> GetAll()
        {
            return await _museumContext.Artworks.ToListAsync();
        }

        public async Task<Artwork> Get(int id)
        {
            return await _museumContext.Artworks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Add(Artwork artwork)
        {
            await _museumContext.Artworks.AddAsync(artwork);
            await _museumContext.SaveChangesAsync();
            return artwork.Id;
        }

        public async Task Update(Artwork artwork)
        {
            Artwork artworkDB = await _museumContext.Artworks.FirstOrDefaultAsync(x => x.Id == artwork.Id);
            artworkDB.Name = artwork.Name;
            artworkDB.Image = artwork.Image;
            artworkDB.Price = artwork.Price;
            artworkDB.Rating = artwork.Rating;
            await _museumContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Artwork artworkDB = await _museumContext.Artworks.FirstOrDefaultAsync(x => x.Id == id);
            _museumContext.Artworks.Remove(artworkDB);
            await _museumContext.SaveChangesAsync();
        }
    }
}
