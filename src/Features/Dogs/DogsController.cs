namespace Dogstagram.Features.Dogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dogstagram.Features.Dogs.Models;
    using Dogstagram.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class DogsController : ApiController
    {
      private readonly IDogsService dogsService;

      public DogsController(IDogsService dogsService) => this.dogsService = dogsService;

      [HttpGet]
      public async Task<IEnumerable<DogListingServiceModel>> Mine()
      {
        var userId = this.User.GetId();

        var dogs = await this.dogsService.ByUser(userId);

        return dogs;
      }

      [HttpGet]
      public async Task<ActionResult<DogDetailsServiceModel>> Details(int id)
      => await this.dogsService.Details(id);

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      public async Task<IActionResult> Create(CreateDogsResponseModel model)
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
