namespace Dogstagram.Features.Dogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dogstagram.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

  public class DogsController : ApiController
  {
    private readonly IDogsService dogsService;

    public DogsController(IDogsService dogsService) => this.dogsService = dogsService;

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<DogListingResponseModel>> Mine()
    {
      var userId = this.User.GetId();

      var dogs = await this.dogsService.ByUser(userId);

      return dogs;
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateDogsRequestModel model)
    {
      var userId = this.User.GetId();

      var id = await this.dogsService.Create(
        model.ImagesUrl,
        model.Description,
        userId);

      return this.Created(nameof(this.Create), id);
    }
  }
}
