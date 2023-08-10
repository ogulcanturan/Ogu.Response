using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Api
{
    public class SampleModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DefaultValue("1")]
        public string? Name { get; set; }

        [Required]
        [Range(1,120)]
        public int Age { get; set; } = 0;
    }
}