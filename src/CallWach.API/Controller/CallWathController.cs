using CallWach.Domain.Aggregates;
using CallWatch.Application.UseCases.GetAllCalls;
using CallWatch.Application.UseCases.GetCallInfo;
using CallWatch.Application.UseCases.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CallWach.API.Controller;

public class CallWathController(
  ILoginUseCase loginUseCase,
  IGetAllCallsUseCase getAllCallsUseCase,
  IGetCallInfoUseCase getCallInfoUseCase
  )
{
  private readonly ILoginUseCase _loginUseCase = loginUseCase;
  private readonly IGetAllCallsUseCase _getAllCallsUseCase = getAllCallsUseCase;
  private readonly IGetCallInfoUseCase _getCallInfoUseCase = getCallInfoUseCase;
  private const string BASEURL = "https://gestaox.aec.com.br/Chamados/AtividadesChamadosV2";
  private readonly ChromeOptions _options = new();

  public void Execute()
  {
    _options.AddArgument("--start-maximized");
    using var driver = new ChromeDriver(_options);

    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

    try
    {
      driver.Navigate().GoToUrl(BASEURL);
      var title = driver.Title;

      _loginUseCase.Execute("natanael.santos", "Na118629*963,", wait);

      if (title == "Bem vindo ao sistema GestaoX - Service Desk")
      {
        driver.Navigate().GoToUrl(BASEURL);

        foreach (var row in _getAllCallsUseCase.Execute(wait))
        {
          var cells = row.FindElements(By.TagName("td"));
          if (cells.Count > 0)
          {
            var callInfo = _getCallInfoUseCase.Execute(cells);
            var validatePercentage = CallAggregate.ValidatePercentage(callInfo.Percentage);

            if (validatePercentage)
            {
              Console.WriteLine($"Chamado quase estourando, responsável: {callInfo.Responsible}, \nPercentual: {callInfo.Percentage} \n{callInfo.Code}");
              Console.WriteLine("---------------------------");
            }
          }
        }
        Console.WriteLine("Título da página: " + title);
        Console.ReadLine();
      }
      Console.WriteLine($"Título da página: {title}");
      Console.ReadLine();
    }
    catch (Exception ex)
    {
      Console.WriteLine("Ocorreu um erro: " + ex.Message);
    }
  }
}
