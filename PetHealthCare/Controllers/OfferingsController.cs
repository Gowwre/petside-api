using Microsoft.AspNetCore.Mvc;
using PetHealthCare.Model.DTO.Request;
using PetHealthCare.Model.DTO.Response;
using PetHealthCare.Services;

namespace PetHealthCare.Controllers;

[Route("api/offers")]
[ApiController]
public class OfferingsController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IOfferingsService _offeringsService;

    public OfferingsController(IOfferingsService offeringsService, ILogger<OfferingsController> logger)
    {
        _offeringsService = offeringsService;
        _logger = logger;
    }

    [HttpPut("updateOffers/{id}")]
    public async Task<ActionResult<ResultResponse<OfferResponseDTO>>> UpdateOffersInfomation(Guid id, OfferRequestDTO offeringsDTO)
    {
        return Ok(await _offeringsService.UpdateOfferingsAsync(id, offeringsDTO));
    }

    [HttpGet("getInformation/{id}")]
    public async Task<ActionResult<ResultResponse<OfferResponseDTO>>> GetInformation(Guid id)
    {
        return Ok(await _offeringsService.GetOfferingsAsync(id));
    }

    [HttpGet("getAllInformation")]
    public ActionResult<List<OfferResponseDTO>> GetAllUser()
    {
        return Ok(_offeringsService.GetAllOfferings());
    }

    [HttpPost("CreateInformation/{providerId}")]
    public async Task<ActionResult<ResultResponse<OfferResponseDTO>>> CreateOfferInformation(Guid providerId, OfferRequestDTO offeringsDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(await _offeringsService.CreateOfferingsAsync(offeringsDTO, providerId));
    }
}