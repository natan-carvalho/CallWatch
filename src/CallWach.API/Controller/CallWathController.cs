using CallWach.Domain.Aggregates;
using CallWatch.Application.UseCases.GetAllCalls;
using CallWatch.Application.UseCases.GetCallInfo;
using CallWatch.Application.UseCases.Login;
using CallWatch.Domain.Interfaces;
using CallWatch.Domain.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CallWach.API.Controller;

public class CallWathController(
  ILoginUseCase loginUseCase,
  IGetAllCallsUseCase getAllCallsUseCase,
  IGetCallInfoUseCase getCallInfoUseCase,
  IUsersReadOnlyRepository userRepository,
  IMessageSender messageSender
  )
{
  private readonly ILoginUseCase _loginUseCase = loginUseCase;
  private readonly IGetAllCallsUseCase _getAllCallsUseCase = getAllCallsUseCase;
  private readonly IGetCallInfoUseCase _getCallInfoUseCase = getCallInfoUseCase;
  private readonly IUsersReadOnlyRepository _userRepository = userRepository;
  private readonly IMessageSender _messageSender = messageSender;
  private const string BASEURL = "https://gestaox.aec.com.br/Chamados/AtividadesChamadosV2";
  private const int TimerValue = 5;
  private readonly ChromeOptions _options = new();

  public async Task Execute()
  {
    // _options.AddArgument("--start-maximized");
    _options.AddArgument("--headless");
    _options.AddArgument("--disable-gpu");
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

        while (true)
        {
          Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Verificando chamados...");

          foreach (var row in _getAllCallsUseCase.Execute(wait))
          {
            var cells = row.FindElements(By.TagName("td"));
            if (cells.Count > 0)
            {
              var callInfo = _getCallInfoUseCase.Execute(cells);
              var validatePercentage = CallAggregate.ValidatePercentage(callInfo.Percentage);

              if (validatePercentage && callInfo.Status != "PENDENTE USUÁRIO")
              {
                var user = await _userRepository.GetByName(callInfo.Responsible.ToLower());
                _messageSender.Send(user!.Number, callInfo.Requester, callInfo.Service, callInfo.Service);
              }
            }
          }

          Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Verificação concluída. Aguardando próximo ciclo...");
          await Task.Delay(TimeSpan.FromMinutes(TimerValue));
          driver.Navigate().GoToUrl(BASEURL);
        }
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
