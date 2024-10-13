using System.ComponentModel.DataAnnotations;

namespace event_driven_backend.Models
{
    public class UserCommunity
    {
        [Key]
        public int ID { get; set; }
        public required User User { get; set; }
        public required Community Community { get; set; }
    }
}
