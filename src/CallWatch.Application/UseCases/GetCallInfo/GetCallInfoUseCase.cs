using System.Collections.ObjectModel;
using CallWach.Domain.Entities;
using OpenQA.Selenium;

namespace CallWatch.Application.UseCases.GetCallInfo;

public class GetCallInfoUseCase : IGetCallInfoUseCase
{
  public Called Execute(ReadOnlyCollection<IWebElement> cells)
  {
    return new Called
    {
      Code = cells[0].Text,
      Requester = cells[8].Text,
      Responsible = cells[9].Text,
      Service = cells[11].Text,
      Percentage = cells[18].Text,
      Status = cells[20].Text
    };
  }
}
