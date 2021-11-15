namespace ScoreTracker.Application.Contracts;

using Domain.Models;

public interface ICurrentUserAccessor
{
  User CurrentUser { get; }

  Task SetCurrentUser(User user);
}