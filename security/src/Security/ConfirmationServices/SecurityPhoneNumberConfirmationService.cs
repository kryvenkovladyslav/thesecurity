using Microsoft.Extensions.Logging;
using Security.Abstract;
using System.Threading.Tasks;

namespace Security
{
    public class SecurityPhoneNumberConfirmationService : IPhoneNumberConfirmationService
    {
        protected ILogger<SecurityPhoneNumberConfirmationService> Logger { get; private init; }

        public SecurityPhoneNumberConfirmationService(ILogger<SecurityPhoneNumberConfirmationService> logger)
        {
            this.Logger = logger;
        }

        public virtual Task SendConfirmationMessage(IPhoneNumberConfirmationMessage confirmationMessage)
        {
            this.Logger.LogInformation("--- SMS Starts ---");

            this.Logger.LogInformation($"To: {confirmationMessage.PhoneNumber}");
            this.Logger.LogInformation($"Subject: {confirmationMessage.Subject}");

            foreach (string str in confirmationMessage.Body)
            {
                this.Logger.LogInformation(str);
            }

            this.Logger.LogInformation("--- SMS Ends ---");

            return Task.CompletedTask;
        }
    }
}