using System.Net;

namespace AshersRoleplayCommands
{
    public class VersionChecker
    {
        private readonly string _versionCheckUrl;

        public VersionChecker(string versionCheckUrl)
        {
            _versionCheckUrl = versionCheckUrl;
        }

        public string GetRemoteVersion()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string remoteVersion = client.DownloadString(_versionCheckUrl).Trim();
                    return remoteVersion;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
