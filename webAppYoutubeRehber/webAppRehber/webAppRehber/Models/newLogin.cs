using System.ComponentModel.DataAnnotations;

namespace webAppRehber.Models
{
    public class newLogin
    {
        [Key]
        public int ID { get; set; }

        [Required,MaxLength(20)]
        public string Email  { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
