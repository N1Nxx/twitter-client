using System.Threading.Tasks;
using Skrc.Twitter.Model;

namespace Skrc.Twitter.Client
{
    public interface ITwitterClient
    {
        Task<AccessTokenResponse> GetAccessTokenAsync();
        Task<LookupResponse> LookupUserAsync(string token, string username);
        Task<TimelineResponse> GetUserTimelineAsync(string token, string userId);
    }
}