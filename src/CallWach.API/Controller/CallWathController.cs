using CallWatch.Application.UseCases.GetAllCalls;
using CallWatch.Application.UseCases.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CallWach.API.Controller;

public class CallWathController(
  ILoginUseCase loginUseCase,
  IGetAllCallsUseCase getAllCallsUseCase
  )
{
  private readonly ILoginUseCase _loginUseCase = loginUseCase;
  private readonly IGetAllCallsUseCase _getAllCallsUseCase = getAllCallsUseCase;
  private const string BASEURL = "https://gestaox.aec.com.br/Chamados/AtividadesChamadosV2";
  private readonly ChromeOptions _options = new();

  public void Execute()
  {
    using var driver = new ChromeDriver(_options);
    _options.AddArgument("--start-maximized");

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
            Console.WriteLine("Código: " + cells[0].Text);
            Console.WriteLine("Solicitante: " + cells[8].Text);
            Console.WriteLine("Responsável: " + cells[9].Text);
            Console.WriteLine("Serviço: " + cells[11].Text);
            Console.WriteLine("Porcentagem: " + cells[18].Text);
            Console.WriteLine("Status: " + cells[20].Text);
            Console.WriteLine("---------------------------");
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
