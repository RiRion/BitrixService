namespace BitrixService.Models.Config
{
    public class BitrixConfig
    {
        public string BaseUri { get; }
        public string BasePath { get; }
        public string Login { get; }
        public string Password { get; }

        public BitrixConfig(
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