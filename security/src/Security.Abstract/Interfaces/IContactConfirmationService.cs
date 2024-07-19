using System.Threading.Tasks;

namespace Security.Abstract
{
    public interface IContactConfirmationService<TConfirmationMessage>
        where TConfirmationMessage : IConfirmationMessage
    {
        public Task SendConfirmationMessage(TConfirmationMessage confirmationMessage);
    }
}