using Application.Common.Helpers;

namespace Infrastructure.Identity.Extensions
{
    public static class IdentityResultExtensions
    {
        public static ApiResponse ToApplicationResult(bool isSucceeded, IEnumerable<string> errors)
        {
            return isSucceeded
                ? ApiResponse.Success()
                : ApiResponse.Failure(errors);
        }
    }
}
