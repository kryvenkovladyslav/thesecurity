using Microsoft.AspNetCore.Identity;

namespace Security.EntityFrameworkStores
{
    public static class IdentityErrorDescriberExtensions
    {
        public static IdentityError StorageFailure(this IdentityErrorDescriber result)
        {
            return new IdentityError
            {
                Code = nameof(StorageFailure),
                Description = "An error occurred while saving changes to the Storage"
            };
        }
    }
}