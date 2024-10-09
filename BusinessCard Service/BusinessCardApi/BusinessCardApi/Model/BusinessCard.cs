using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessCardApi.Model
{
    public class BusinessCard
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
        [MaxLength(5000)]
        public string? photo { get; set; } = null;
        [Required]
        public string address { get; set; }

    }
}
