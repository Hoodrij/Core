using System.Threading.Tasks;
using UnityEngine;

namespace Core.Units
{
    public interface IAssets
    {
        Task<Object> Load(string path);
        Task<T> Load<T>(string path) where T : Component;
    }
}