using OpenQA.Selenium.Support.UI;

namespace CallWatch.Application.UseCases.Login;

public interface ILoginUseCase
{
  void Execute(string username, string password, WebDriverWait wait);
}
