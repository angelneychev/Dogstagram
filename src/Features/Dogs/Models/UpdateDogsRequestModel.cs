namespace Dogstagram.Features.Dogs.Models
{
  using System.ComponentModel.DataAnnotations;

  using static Data.Validation.Dog;

  public class UpdateDogsRequestModel
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; }
  }
}
