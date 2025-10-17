using CallWatch.Application.UseCases.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CallWach.API.Controller;

public class CallWathController(ILoginUseCase loginUseCase)
{
  private readonly ILoginUseCase _loginUseCase = loginUseCase;
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

      _loginUseCase.Execute("natanael.santos", "Na118629*963,", wait);

      if (driver.Title == "Bem vindo ao sistema GestaoX - Service Desk")
      {
        driver.Navigate().GoToUrl(BASEURL);

        var inputCode = wait.Until(d => d.FindElement(By.Id("ctl00_MainContent_gridchamados_ctl00_ctl02_ctl02_FilterTextBox_CODIGO")));
        inputCode.SendKeys("AIR0");
        inputCode.SendKeys(Keys.Enter);

        Thread.Sleep(3000); // Espera 3 segundos para garantir que a tabela foi carregada

        var table = wait.Until(d => d.FindElement(By.Id("ctl00_MainContent_gridchamados_ctl00")));
        var rows = table.FindElements(By.TagName("tr"));
        foreach (var row in rows)
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

        Console.WriteLine("Título da página: " + driver.Title);
        Console.ReadLine();
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine("Ocorreu um erro: " + ex.Message);
    }
  }
}
