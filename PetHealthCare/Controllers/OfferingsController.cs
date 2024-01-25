using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;
[Route("api/offers")]
[ApiController]
public class OfferingsController : ControllerBase
{
    private readonly IOfferingsService _offeringsService;
    private readonly ILogger _logger;

    public OfferingsController(IOfferingsService offeringsService, ILogger<OfferingsController> logger)
    {
        _offeringsService = offeringsService;
        _logger = logger;
    }

    [HttpPut("updateOffers/{id}")]
    public async Task<IActionResult> UpdateOffersInfomation(Guid id, OfferRequestDTO offeringsDTO)
    {
        return Ok(await _offeringsService.UpdateOfferingsAsync(id, offeringsDTO));
    }

    [HttpGet("getInformation/{id}")]
    public async Task<IActionResult> GetInfomation(Guid id)
    {
        return Ok(await _offeringsService.GetOfferingsAsync(id));
    }

    [HttpGet("getAllInformation")]
    public IActionResult GetAllUser()
    {
        return Ok(_offeringsService.GetAllOfferings());
    }

    [HttpPost("CreateInformation/{providerId}")]
    public async Task<IActionResult> CreateOfferInformation(Guid providerId, OfferRequestDTO offeringsDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(await _offeringsService.CreateOfferingsAsync(offeringsDTO, providerId));
    }

}
