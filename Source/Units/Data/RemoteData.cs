#if REMOTE_CONFIG
using Unity.RemoteConfig;
using UnityEngine;

namespace Core
{
    public class RemoteData : Unit
    {
        [inject] Life Life;
        
        public int Version { get; private set; }

        private struct UserAttributes { }
        private struct AppAttributes { }

        public RemoteData()
        {
            ConfigManager.FetchCompleted += ApplyRemoteSettings;
            Life.OnQuit.Listen(() => ConfigManager.FetchCompleted -= ApplyRemoteSettings);
            
            ConfigManager.FetchConfigs(new UserAttributes(), new AppAttributes());
        }
        
        private void ApplyRemoteSettings(ConfigResponse configResponse) 
        {
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
            
            string dataJson = ConfigManager.appConfig.GetString("data");
            JsonUtility.FromJsonOverwrite(dataJson, this);
        }
    }
}
#endif
