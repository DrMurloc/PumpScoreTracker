namespace ScoreTracker.Mediation.Queries;

using Core.Models;
using MediatR;

public sealed class GetChartsQuery : IRequest<IEnumerable<Chart>> { }