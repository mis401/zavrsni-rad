namespace event_driven_backend.DTOs
{
    public class NewUserDTO
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    
    public class NewCommunityDTO
    {
        public required string Name { get; set; }

        public required int userId { get; set; }
    }

    public class NewEventDTO
    {
        public required string Name { get; set; }
        public EventTheme? Color { get; set; } = EventTheme.WHITE;
        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
        public required int calendarId { get; set; }
    }

    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class NewDocumentDTO
    {
        public required string Name { get; set; }
        public required int creatorId { get; set; }
        public required int calendarId { get; set; }
    }
    
    public class UpdateDocumentDTO
    {
        public required int DocumentID { get; set; }
        public required string Name { get; set; }
        public required string Text { get; set; }
    }
}
