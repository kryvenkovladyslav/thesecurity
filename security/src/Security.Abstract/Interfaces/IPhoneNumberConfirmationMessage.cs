namespace Security.Abstract
{
    public interface IPhoneNumberConfirmationMessage : IConfirmationMessage
    {
        public string PhoneNumber { get; init; }
    }
}