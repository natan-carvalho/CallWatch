namespace CallWatch.Domain.Interfaces;

public interface IMessageSender
{
  void Send(string responsibleNumber, string requester, string service, string percentage);
}
