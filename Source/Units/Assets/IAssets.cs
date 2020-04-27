using System.Threading.Tasks;
using UnityEngine;

namespace Core.Assets
{
    public interface IAssets
    {
        Task<Object> Load(string path);
        Task<T> Load<T>(string path) where T : Component;
        
        Task<T> Spawn<T>(string path, bool persistent = false) where T : class;
        Task<GameObject> Spawn(string path, bool persistent = false);
    }
}