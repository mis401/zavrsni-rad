using System.ComponentModel.DataAnnotations;

namespace event_driven_backend.Models
{
    public class Community
    {
        [Key]
        public int ID { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        //public List<User> Users { get; set; } m:n?
        public required User Creator { get; set; }
        public required Calendar Calendar { get; set; }
        //vise usera m:n, usera koji ga je kreirao 1:n, pripada mu kalendar 1:1
        
        
        public required String Code { get; set; }

        [JsonIgnore]
        public List<UserCommunity> UserCommunities { get; set; } = new List<UserCommunity>();
    }
}
