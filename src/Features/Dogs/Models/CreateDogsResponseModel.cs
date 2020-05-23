namespace Dogstagram.Features.Dogs.Models
{
  using System.ComponentModel.DataAnnotations;

  using static Data.Validation.Dog;

  public class CreateDogsResponseModel
  {
    [Required]
    public string ImagesUrl { get; set; }

    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; }
  }
}
