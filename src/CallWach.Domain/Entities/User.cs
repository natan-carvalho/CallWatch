namespace CallWach.Domain.Entities;

public class User
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string Name { get; set; } = string.Empty;
  public string Number { get; set; } = string.Empty;
}
