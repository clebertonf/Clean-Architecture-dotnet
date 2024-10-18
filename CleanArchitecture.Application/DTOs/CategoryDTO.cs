using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CleanArchitecture.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}
