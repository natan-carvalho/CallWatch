using System.Collections.ObjectModel;
using CallWach.Domain.Entities;
using OpenQA.Selenium;

namespace CallWatch.Application.UseCases.GetCallInfo;

public interface IGetCallInfoUseCase
{
  Called Execute(ReadOnlyCollection<IWebElement> cells);
}
