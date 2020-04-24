#if REMOTE_CONFIG
using System.Threading.Tasks;
using Unity.RemoteConfig;
using UnityEngine;

namespace Core
{
    public class RemoteData : Unit
    {
        [inject] Life Life;
        
        public int Version { get; private set; }
        public bool IsInitialized { get; private set; }

        private struct UserAttributes { }
        private struct AppAttributes { }

        protected RemoteData()
        {
            ConfigManager.FetchCompleted += ApplyRemoteSettings;
            Life.OnQuit.Listen(() => ConfigManager.FetchCompleted -= ApplyRemoteSettings);
            
            ConfigManager.FetchConfigs(new UserAttributes(), new AppAttributes());
        }
        
        private void ApplyRemoteSettings(ConfigResponse configResponse)
        {
            if (configResponse.status != ConfigRequestStatus.Success)
                return;
            
            // Conditionally update settings, depending on the response's origin:
            switch (configResponse.requestOrigin) {
                case ConfigOrigin.Default:
                    Debug.Log ("No settings loaded this session; using default values.");
                    break;
                case ConfigOrigin.Cached:
                    Debug.Log ("No settings loaded this session; using cached values from a previous session.");
                    break;
                case ConfigOrigin.Remote:
                    Debug.Log ("New settings loaded this session; update values accordingly.");
                    break;
            }

            Version = ConfigManager.appConfig.GetInt("version");

            string dataJson = ConfigManager.appConfig.GetJson("data");
            JsonUtility.FromJsonOverwrite(dataJson, this);

            IsInitialized = true;
        }

        public async Task WaitInit()
        {
            await new WaitUntil(() => IsInitialized);
        }
    }
}
#endif
