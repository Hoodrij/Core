﻿using System.Threading.Tasks;
using Core.Abstract;
using UnityEngine;

namespace Core.Units.Assets
{
    public class AddressableAssets : Unit, IAssets
    {
        public Object Load(string path)
        {
            throw new System.NotImplementedException();
        }

        public T Load<T>(string path) where T : Component
        {
            throw new System.NotImplementedException();
        }

        public Task<T> LoadAsync<T>(string path) where T : Component
        {
            throw new System.NotImplementedException();
        }

        public T Spawn<T>(string path, bool persistent = false) where T : class
        {
            throw new System.NotImplementedException();
        }

        public GameObject Spawn(string path, bool persistent = false)
        {
            throw new System.NotImplementedException();
        }
    }
}