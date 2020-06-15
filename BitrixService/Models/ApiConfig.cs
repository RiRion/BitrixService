using System.Collections.Generic;

namespace BitrixService.Models
{
    public class ApiConfig
    {
        public string BaseUri { get; }
        public string PrefixAuth { get; }
        public string PrefixApi { get; }
        public string Login { get; }
        public string Password { get; }
        public Dictionary<string, string> FormData { get; }

        public ApiConfig(
            string baseUri,
            string prefixAuth,
            string prefixApi,
            string login,
            string password)
        {
            BaseUri = baseUri;
            PrefixAuth = prefixAuth;
            Login = login;
            Password = password;
            FormData = new Dictionary<string, string>()
            {
                {"USER_LOGIN", Login},
                {"USER_PASSWORD", Password},
                {"AUTH_FORM", "Y"},
                {"TYPE", "AUTH"}
            };
        }
    }
}