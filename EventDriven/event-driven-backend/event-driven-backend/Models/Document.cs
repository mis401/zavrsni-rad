namespace event_driven_backend.Models;
public class Document
{
    [Key]
    public int ID { get; set; }
    public required string Name { get; set; }
    public required string Text { get; set; } = String.Empty;
    public required User Creator { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required Calendar Calendar { get; set; }

    public required DateTime UpdatedAt { get; set; }

}
