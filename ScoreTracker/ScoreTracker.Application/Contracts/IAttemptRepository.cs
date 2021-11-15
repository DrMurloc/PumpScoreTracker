namespace ScoreTracker.Application.Contracts;

using Core.Models;
using Domain.Models;
using Domain.ValueTypes;

public interface IAttemptRepository
{
  Task CreateAttempt(User user, Chart chart, GradeValueType grade, CancellationToken cancellationToken = default);
}