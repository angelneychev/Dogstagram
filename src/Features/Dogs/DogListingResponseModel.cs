using System.ComponentModel.DataAnnotations;

namespace Dogstagram.Features.Dogs
{
  public class DogListingResponseModel
  {
    public int Id { get; set; }

    public string ImagesUrl { get; set; }
  }
}
