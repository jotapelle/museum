using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Museum.Api.ViewModels;
using Museum.Api.ViewModels.MapperProfiles;
using Museum.BusinessLogic.BLs.Contracts;
using Museum.BusinessLogic.BusinessObjects;

namespace Museum.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtworksController : ControllerBase
    {
        private readonly IArtworksBL _artworksBL;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public ArtworksController(IArtworksBL artworksBL)
        {
            _artworksBL = artworksBL;
            _mapperConfiguration = new MapperConfiguration(m =>
            {
                m.AddProfile<ArtworksBOProfile>();
                m.AddProfile<ArtworksVMProfile>();
                m.AddProfile<ArtworksExtVMProfile>();
            });
            _mapper = _mapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<ArtworkVM> result = _mapper.Map<List<ArtworkVM>>(await _artworksBL.GetAll());
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting list artworks");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                ArtworkVM result = _mapper.Map<ArtworkVM>(await _artworksBL.Get(id));
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting artwork by id: " + id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ArtworkExtVM artworkExtVM)
        {
            try
            {
                if (artworkExtVM == null) return BadRequest("Error artwork cannot be null");
                ArtworkVM artworkVM = _mapper.Map<ArtworkVM>(artworkExtVM);
                ArtworkBO artworkBO = _mapper.Map<ArtworkBO>(artworkVM);
                int result = await _artworksBL.Add(artworkBO);
                artworkVM.Id = result;
                return Ok(artworkVM);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new artwork");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ArtworkVM artworkVM)
        {
            try
            {
                if (artworkVM == null) return BadRequest("Error artwork cannot be null");
                bool existArt = await _artworksBL.Get(artworkVM.Id) != null;
                if (!existArt) return BadRequest("Error artwork by id: " + artworkVM.Id + " doesn't exist");
                ArtworkBO artworkBO = _mapper.Map<ArtworkBO>(artworkVM);
                await _artworksBL.Update(artworkBO);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating artwork");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool existArt = await _artworksBL.Get(id) != null;
                if (!existArt) return BadRequest("Error artwork by id: " + id + " doesn't exist");
                await _artworksBL.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting artwork by id: " + id);
            }
        }
    }
}