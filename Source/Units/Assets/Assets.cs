using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Assets
{
    public class Assets : Unit, IAssets
    {
        public async Task<Object> Load(string path)
        {
            return await Resources.LoadAsync(path);
        }

        public async Task<T> Load<T>(string path) where T : Component
        {
            return await Resources.LoadAsync<T>(path) as T;
        }

        public async Task<T> Spawn<T>(string path, bool persistent = false) where T : class
        {
            Object prefab = await Load(path);
            GameObject go = Object.Instantiate(prefab) as GameObject;

            if (persistent) 
                Object.DontDestroyOnLoad(go);
            if (typeof(T) == typeof(GameObject)) 
                return go as T;

            return go.GetComponent(typeof(T)) as T;
        }

        public Task<GameObject> Spawn(string path, bool persistent = false)
        {
            return Spawn<GameObject>(path, persistent);
        }
    }
}