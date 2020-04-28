#if ADDRESSABLES
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Assets
{
    public class AddressableAssets : Unit, IAssets
    {
        public async Task<T> Load<T>(string path) where T : Component
        {
            bool isCompleted = false;
            T result = null;
            Addressables.LoadAssetAsync<T>(path).Completed += handle =>
            {
                result = handle.Result;
                isCompleted = true;
            };
            await new WaitUntil(() => isCompleted);

            return result;
        }
        
        public async Task<Object> Load(string path)
        {
            AsyncOperationHandle<Object> asyncOperationHandle = Addressables.LoadAssetAsync<Object>(path);
            await new WaitUntil(() => asyncOperationHandle.IsDone);

            return asyncOperationHandle.Result;
        }
    }
}
#endif
