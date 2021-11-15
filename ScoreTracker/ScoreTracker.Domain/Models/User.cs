namespace ScoreTracker.Domain.Models;

public sealed class User
{
  public User(string email)
  {
    Email = email;
  }

  public string Email { get; }
}