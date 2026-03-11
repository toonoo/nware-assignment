using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace NWARE.API.Authorization
{
    public interface IAuthorizationService
    {
        bool ValidateApiKey(string apiKey);
    }

    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _configuration;
        private readonly List<string> _validApiKeys;

        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _validApiKeys = new List<string>();
            LoadValidApiKeys();
        }

        private void LoadValidApiKeys()
        {
            var apiKeys = _configuration.GetSection("Authorization:ApiKeys").Get<string[]>();
            if (apiKeys != null)
            {
                _validApiKeys.AddRange(apiKeys);
            }
        }

        public bool ValidateApiKey(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return false;

            return _validApiKeys.Contains(apiKey);
        }
    }
}
