namespace ScoreTracker.DataAccess.Entities;

public sealed class UserEntity
{
  public string Email { get; set; } = "";
  public Guid Id { get; set; }
}