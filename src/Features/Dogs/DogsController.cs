namespace Dogstagram.Features.Dogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dogstagram.Features.Dogs.Models;
    using Dogstagram.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using static Infrastructure.WebContstants;

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
      [Route(Id)]
      public async Task<ActionResult<DogDetailsServiceModel>> Details(int id)
      => await this.dogsService.Details(id);

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

      [HttpPut]
      public async Task<ActionResult> Update(UpdateDogsRequestModel model)
      {
        var userId = this.User.GetId();

        var update = await this.dogsService.Update(
          model.Id,
          model.Description,
          userId);
        if (!update)
        {
          return this.BadRequest();
        }

        return this.Ok();
      }

      [HttpDelete]
      [Route(Id)]
      public async Task<ActionResult> Delete(int id)
      {
        var userId = this.User.GetId();

        var delete = await this.dogsService.Delete(id, userId);

        if (!delete)
        {
          return this.BadRequest();
        }

        return this.Ok();
      }
  }
}
