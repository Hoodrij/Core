#if ADDRESSABLES
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Assets
{
    public class AddressableAssets : Unit, IAssets
    {
        public async Task<Object> Load(string path)
        {
            return await Addressables.LoadAssetAsync<Object>(path).Task;
        }
        
        public async Task<T> Load<T>(string path) where T : Component
        {
            GameObject gameObject = await Addressables.LoadAssetAsync<GameObject>(path).Task;
            return gameObject.GetComponent<T>();
        }
    }
}
#endif
