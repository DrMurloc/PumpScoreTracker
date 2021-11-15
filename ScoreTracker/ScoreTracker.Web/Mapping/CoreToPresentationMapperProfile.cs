namespace ScoreTracker.Web.Mapping;

using AutoMapper;
using Core.Models;
using Dtos;

public sealed class CoreToPresentationMapperProfile : Profile
{
  public CoreToPresentationMapperProfile()
  {
    CreateMap<Chart, ChartDto>();
  }
}