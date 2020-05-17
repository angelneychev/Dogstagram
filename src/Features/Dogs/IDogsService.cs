using System.Collections.Generic;

namespace Dogstagram.Features.Dogs
{
    using System.Threading.Tasks;

  public interface IDogsService
  {
    public Task<int> Create(string imagesUrl, string description, string userId);

    public Task<IEnumerable<DogListingResponseModel>> ByUser(string userId);
  }
}
