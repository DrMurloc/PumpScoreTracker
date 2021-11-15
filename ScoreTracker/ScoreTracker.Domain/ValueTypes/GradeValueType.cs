namespace ScoreTracker.Domain.ValueTypes;

public struct GradeValueType
{
  private GradeValueType(string letter, bool isPassing)
  {
    Letter = letter;
    IsPassing = isPassing;
  }

  public string Letter { get; }
  public bool IsPassing { get; }

  private static readonly string[] _validLetters = { "A", "B", "C", "D", "F", "S", "SS", "SSS" };

  public static bool TryParse(string letter, bool isPassing, out GradeValueType result)
  {
    result = new GradeValueType("F", false);
    if (!_validLetters.Contains(letter, StringComparer.OrdinalIgnoreCase))
    {
      return false;
    }

    result = new GradeValueType(letter.ToUpper(), isPassing);
    return true;
  }
}