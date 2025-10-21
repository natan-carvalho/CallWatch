namespace CallWach.Domain.Aggregates;

public class CallAggregate
{
  public static bool ValidatePercentage(string Percentage)
  {
    if (int.TryParse(Percentage.TrimEnd('%'), out int percentValue))
    {
      return percentValue >= 60;
    }
    return false;
  }
}
