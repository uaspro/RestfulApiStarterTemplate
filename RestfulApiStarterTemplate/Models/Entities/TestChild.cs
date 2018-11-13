using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestfulApiStarterTemplate.Models.Entities.Base;

namespace RestfulApiStarterTemplate.Models.Entities
{
    [Table("tblTestChildren", Schema = "dbo")]
    public class TestChild : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }
    }
}
