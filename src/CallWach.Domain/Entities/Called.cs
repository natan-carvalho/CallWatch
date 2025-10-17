namespace CallWach.Domain.Entities;

public class Called
{
  public string Code { get; set; } = string.Empty;
  public string Requester { get; set; } = string.Empty;
  public string Responsible { get; set; } = string.Empty;
  public string Service { get; set; } = string.Empty;
  public string Percentage { get; set; } = string.Empty;
  public string Status { get; set; } = string.Empty;
}
