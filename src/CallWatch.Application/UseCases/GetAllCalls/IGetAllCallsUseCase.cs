using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CallWatch.Application.UseCases.GetAllCalls;

public interface IGetAllCallsUseCase
{
  ReadOnlyCollection<IWebElement> Execute(WebDriverWait wait);
}
