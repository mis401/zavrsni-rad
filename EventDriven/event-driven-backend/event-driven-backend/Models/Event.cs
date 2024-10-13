using System.ComponentModel.DataAnnotations;

namespace event_driven_backend.Models
{
    public enum EventTheme
    {
        RED,
        BLUE,
        GREEN,
        YELLOW,
        PURPLE,
        ORANGE,
        PINK,
        GREY,
        BLACK,
        WHITE
    }
    public class Event
    {
        [Key]
        public int ID { get; set; }
        public required string Name { get; set; }
        public EventTheme? Color { get; set; } 

        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
        public required Calendar Calendar { get; set; }

        //ima kalendar

    }
}
