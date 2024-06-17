using Microsoft.Extensions.Logging;
using Security.Abstract;
using System.Threading.Tasks;

namespace Security
{
    public class SecurityEmailConfirmationService : IEmailConfirmationService
    {
        protected ILogger<SecurityEmailConfirmationService> Logger { get; private init; }

        public SecurityEmailConfirmationService(ILogger<SecurityEmailConfirmationService> logger)
        {
            this.Logger = logger;
        }

        public virtual Task SendConfirmationMessage(IEmailConfirmationMessage confirmationMessage)
        {
            this.Logger.LogInformation("--- Email Starts ---");

            this.Logger.LogInformation($"To: {confirmationMessage.Email}");
            this.Logger.LogInformation($"Subject: {confirmationMessage.Subject}");

            foreach (string str in confirmationMessage.Body)
            {
                this.Logger.LogInformation(str);
            }

            this.Logger.LogInformation("--- Email Ends ---");

            return Task.CompletedTask;
        }
    }
}