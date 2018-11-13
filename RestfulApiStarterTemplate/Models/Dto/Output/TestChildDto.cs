using RestfulApiStarterTemplate.Models.Dto.Base;

namespace RestfulApiStarterTemplate.Models.Dto.Output
{
    public class TestChildDto : BaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
