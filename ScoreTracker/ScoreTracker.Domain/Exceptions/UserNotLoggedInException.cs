namespace ScoreTracker.Domain.Exceptions;

public sealed class UserNotLoggedInException : Exception
{
  public UserNotLoggedInException() : base("User not currently logged in") { }
}