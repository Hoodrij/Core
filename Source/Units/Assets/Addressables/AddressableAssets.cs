#if ADDRESSABLES
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Assets
{
    public class AddressableAssets : Unit, IAssets
    {
        public async Task<Object> Load(string path)
        { 
            return await Addressables.LoadAssetAsync<Object>(path) as Object;
        }

        public async Task<T> Load<T>(string path) where T : Component
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> Spawn<T>(string path, bool persistent = false) where T : class
        {
            // await Addressables.InstantiateAsync(path);
            
            throw new System.NotImplementedException();
        }

        public async Task<GameObject> Spawn(string path, bool persistent = false)
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif
