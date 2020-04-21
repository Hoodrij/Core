using System.Threading.Tasks;
using UnityEngine;

namespace Core.Assets
{
    public interface IAssets
    {
        Object Load(string path);
        T Load<T>(string path) where T : Component;
        Task<T> LoadAsync<T>(string path) where T : Component;
        T Spawn<T>(string path, bool persistent = false) where T : class;
        GameObject Spawn(string path, bool persistent = false);
    }
}