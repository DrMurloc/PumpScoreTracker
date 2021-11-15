namespace ScoreTracker.UnitTests.Helpers;

using AutoMapper;
using Web.Mapping;

public static class MapperBuilder
{
  public static IMapper BuildMapper()
  {
    return new MapperConfiguration(c => { c.AddMaps(typeof(CoreToPresentationMapperProfile).Assembly); }).CreateMapper();
  }
}