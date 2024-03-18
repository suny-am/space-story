namespace API.Models;

public record class SaveState
{
    public Guid Id { get; set; }
    public string? Data { get; set; }
}