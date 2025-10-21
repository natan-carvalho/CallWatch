using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CallWatch.Application.UseCases.GetAllCalls;

public class GetAllCallsUseCase : IGetAllCallsUseCase
{
  public ReadOnlyCollection<IWebElement> Execute(WebDriverWait wait)
  {
    var inputCode = wait.Until(d => d.FindElement(By.Id("ctl00_MainContent_gridchamados_ctl00_ctl02_ctl02_FilterTextBox_CODIGO")));
    inputCode.SendKeys("AIR0");
    inputCode.SendKeys(Keys.Enter);

    Thread.Sleep(millisecondsTimeout: 5000); // Espera 5 segundos para garantir que a tabela foi carregada

    var table = wait.Until(d => d.FindElement(By.Id("ctl00_MainContent_gridchamados_ctl00")));
    return table.FindElements(By.TagName("tr"));
  }
}