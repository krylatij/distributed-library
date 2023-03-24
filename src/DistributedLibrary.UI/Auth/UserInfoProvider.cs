using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace DistributedLibrary.UI.Auth
{
    public class UserInfoProvider
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserInfoProvider(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> GetUserIdAsync()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

            var userClaim = state.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userClaim == null)
            {
                throw new InvalidOperationException("User id claim is not found");
            }

            return userClaim.Value;
        }
    }
}
