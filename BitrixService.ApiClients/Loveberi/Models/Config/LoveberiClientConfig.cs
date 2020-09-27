namespace BitrixService.ApiClients.Models.Config
{
    public class LoveberiClientConfig
    {
        public string BaseUri { get; }
        public string BasePath { get; }
        public string Login { get; }
        public string Password { get; }

        public LoveberiClientConfig(
            string baseUri,
            string basePath,
            string login,
            string password)
        {
            BaseUri = baseUri;
            BasePath = basePath;
            Login = login;
            Password = password;
        }
    }
}