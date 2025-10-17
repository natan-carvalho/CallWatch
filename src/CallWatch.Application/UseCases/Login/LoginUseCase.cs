using CallWatch.Application.UseCases.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CallWatch.Application.UseCases;

internal class LoginUseCase : ILoginUseCase
{
  public void Execute(string username, string password, WebDriverWait wait)
  {
    var inputUser = wait.Until(d => d.FindElement(By.Id("edtLogin")));
    var inputPass = wait.Until(d => d.FindElement(By.Id("edtsenha")));
    var btnLogin = wait.Until(d => d.FindElement(By.Id("btnLogin")));

    inputUser.SendKeys(username);
    inputPass.SendKeys(password);
    btnLogin.Click();
  }
}
