using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Units
{
    public class Assets : Unit, IAssets
    {
        public async Task<Object> Load(string path)
        {
            return await Resources.LoadAsync(path) as GameObject;
        }

        public async Task<T> Load<T>(string path) where T : Object
        {
            return await Resources.LoadAsync<T>(path) as T;
        }
    }
}