using Microsoft.AspNetCore.Components.Authorization;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Extensions;

public static class AuthenticationStateProviderExtensions
{
    public static async Task<string> GetUsername(this AuthenticationStateProvider provider)
    {
        var user = await provider.GetAuthenticationStateAsync();
        var userName = user.User.Identity?.Name ?? string.Empty;
        return userName;
    }
}