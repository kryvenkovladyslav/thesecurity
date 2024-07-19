namespace Security.Abstract
{
    public interface IConfirmationMessage
    {
        public string Subject { get; init; }

        public string[] Body { get; init; }
    }
}