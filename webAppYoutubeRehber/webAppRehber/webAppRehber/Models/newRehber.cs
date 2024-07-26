using System.ComponentModel.DataAnnotations;

namespace webAppRehber.Models
{
    public class newRehber
    {
        [Key]

        public int ID { get; set; }

        [Required]
        [MaxLength(10)]
        public string YeniKişiAdı { get; set; }

        [Required]
        [MaxLength(15)]
        public string Soyadı{ get; set; }

        [Required]
        [MaxLength(100)]
        public string Adress  { get; set; }

        [Required]

        public string? TelNo { get; set; }

      
        public string? photopath { get; set; }
            
        public newRehber(){
        
        }
    }
}
