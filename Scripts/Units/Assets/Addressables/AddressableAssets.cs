#if ADDRESSABLES
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Units
{
    public class AddressableAssets : Unit, IAssets
    {
        public async Task<Object> Load(string path)
        {
            return await Addressables.LoadAssetAsync<Object>(path).Task;
        }

        public async Task<T> Load<T>(string path) where T : Object
        {
            if (typeof(T).IsSubclassOf(typeof(Component)))
            {
                GameObject gameObject = await Addressables.LoadAssetAsync<GameObject>(path).Task;
                return gameObject.GetComponent<T>();
            }

            return await Addressables.LoadAssetAsync<T>(path).Task;
        }
    }
}
#endif