namespace Security.Abstract
{
    public interface IEmailConfirmationMessage : IConfirmationMessage
    {
        public string Email { get; init; }
    }
}