using System.ComponentModel.DataAnnotations;

namespace SkepERP.Models
{
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime RequestTime { get; set; }

        public string? RequestBody { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public string Method { get; set; }

        public string? Arguments {  get; set; }

        [Required]
        public string ErrorMessage { get; set; }
    }
}
