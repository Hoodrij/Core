using System.Threading.Tasks;
using UnityEngine;

namespace Core.Assets
{
    public interface IAssets
    {
        Task<Object> Load(string path);
        Task<T> Load<T>(string path) where T : Component;
    }
}