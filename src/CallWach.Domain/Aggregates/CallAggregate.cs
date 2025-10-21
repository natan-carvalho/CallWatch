namespace CallWach.Domain.Aggregates;

public class CallAggregate
{
  public static bool ValidatePercentage(string Percentage)
  {
    if (int.TryParse(Percentage.TrimEnd('%'), out int percentValue))
    {
      return percentValue >= 90;
    }
    return false;
  }
}
