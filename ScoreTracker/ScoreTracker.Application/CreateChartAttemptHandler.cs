namespace ScoreTracker.Application;

using Contracts;
using Mediation.Commands;
using MediatR;

public sealed class CreateChartAttemptHandler : IRequestHandler<CreateChartAttemptCommand>
{
  private readonly IAttemptRepository _attemptRepository;
  private readonly IChartRepository _chartRepository;
  private readonly ICurrentUserAccessor _currentUserAccessor;

  public CreateChartAttemptHandler(IChartRepository chartRepository, ICurrentUserAccessor currentUserAccessor, IAttemptRepository attemptRepository)
  {
    _currentUserAccessor = currentUserAccessor;
    _attemptRepository = attemptRepository;
    _chartRepository = chartRepository;
  }

  public async Task<Unit> Handle(CreateChartAttemptCommand request, CancellationToken cancellationToken)
  {
    var user = _currentUserAccessor.CurrentUser;
    var chart = await _chartRepository.GetChart(request.SongName, request.ChartType, request.Level, cancellationToken);
    await _attemptRepository.CreateAttempt(user, chart, request.Grade, cancellationToken);
    return Unit.Value;
  }
}