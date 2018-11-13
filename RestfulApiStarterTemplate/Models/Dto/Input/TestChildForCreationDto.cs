using System.ComponentModel.DataAnnotations;

namespace RestfulApiStarterTemplate.Models.Dto.Input
{
    public class TestChildForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
