using CallWatch.Domain.Interfaces;

namespace CallWatch.Infrastructure.MessageSender;

public class WhatsAppSender : IMessageSender
{
  public void Send(string responsibleNumber, string requester, string service, string percentage)
  {
    throw new NotImplementedException();
  }
}
