using System.Collections.Generic;
using RestfulApiStarterTemplate.Models.Dto.Base;

namespace RestfulApiStarterTemplate.Models.Dto
{
    public class TestDto : BaseDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int CountOfChildren { get; set; }

        public List<TestChildDto> TestChildren { get; set; }
    }
}
