#if REMOTE_CONFIG
using System.Threading.Tasks;
using Unity.RemoteConfig;
using UnityAsync;
using UnityEngine;

namespace Core.Units
{
    public abstract class RemoteData : Unit
    {
        [Inject] Life Life;

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

            Version = ConfigManager.appConfig.GetInt("version");

            string dataJson = ConfigManager.appConfig.GetJson("data");
            JsonUtility.FromJsonOverwrite(dataJson, this);

            IsInitialized = true;
        }

        public async Task WaitInit()
        {
            await Wait.Until(() => IsInitialized);// UnityEngine.WaitUntil(() => IsInitialized);
        }
    }
}
#endif