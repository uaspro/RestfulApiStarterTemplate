using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RestfulApiStarterTemplate.Models.Dto.Base;

namespace RestfulApiStarterTemplate.Models.Dto.Input
{
    public class TestForCreationDto : BaseDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public List<TestChildForCreationDto> TestChildren { get; set; } 
            = new List<TestChildForCreationDto>();
    }
}
