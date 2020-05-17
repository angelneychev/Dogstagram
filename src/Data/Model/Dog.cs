namespace Dogstagram.Data.Model
{
    using System.ComponentModel.DataAnnotations;

    using static Validation.Dog;

  public class Dog
  {
    public int Id { get; set; }
    [Required]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; }

    [Required]
    public string ImagesUrl { get; set; }

    [Required]
    public string UserId { get; set; }

    public User User { get; set; }
  }
}
