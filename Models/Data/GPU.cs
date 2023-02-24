using System.ComponentModel.DataAnnotations;

namespace GPUStoreMVC.Models.Data
{
    public class GPU
    {

        [Key]
        public int GPUID { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [StringLength(50)]
        public string? Chip { get; set; }
        [Required]
        [StringLength(50)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [StringLength(50)]
        public string? Bus { get; set; }
        [Required]
        [StringLength(50)]
        public string? Memory { get; set; }
        public string? GPUImage { get; set; }
    }
}
