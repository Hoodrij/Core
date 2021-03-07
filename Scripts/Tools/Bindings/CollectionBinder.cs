using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Tools.Bindings
{
    public abstract class CollectionBinder<TComponent, TData> : ABinder where TComponent : MonoBehaviour
    {
        [Tooltip("Container for items")] [SerializeField]
        private Transform _container;
        
        private Func<Collection<TComponent, TData>> _getter;
        private Dictionary<TData, TComponent> _items;

        [Tooltip("Prefab to instantiate in collection")] [SerializeField]
        private TComponent _prefab;

        protected override void Bind()
        {
            if (_container == null || _prefab == null || _getter == null)
                return;
            
            Collection<TComponent, TData> collection = _getter.Invoke();
            if (collection.IsEmpty) //clear all items
            {
                foreach (var item in _items)
                    Destroy(item.Value);
                _items.Clear();
            }
            else //fill items
            {
                _items = new Dictionary<TData, TComponent>(_items);

                int index = 0;
                HashSet<object> itemsKeys = new HashSet<object>();
                foreach (TData data in collection)
                {
                    bool isNew = false;
                    //doesn't have -> create new one
                    if (!_items.TryGetValue(data, out TComponent item))
                    {
                        item = Instantiate(_prefab, _container, false);
                        item.transform.localScale = Vector3.one;
                        _items.Add(data, item);
                        isNew = true;
                    }

                    item.transform.SetSiblingIndex(index);
                    collection.SetupAction.Invoke(item, data, isNew);

                    if (!item.gameObject.activeSelf)
                        item.gameObject.SetActive(true);

                    itemsKeys.Add(data);
                    index++;
                }

                //clear items, who doesn't exists in collection
                TData[] itemsToRemove = _items.Keys.Where(key => !itemsKeys.Contains(key)).ToArray();
                foreach (TData itemKey in itemsToRemove)
                {
                    Destroy(_items[itemKey]);
                    _items.Remove(itemKey);
                }
            }
        }

        public void SetPrefab(TComponent prefab)
        {
            _prefab = prefab;
        }

        private void Awake()
        {
            Init(ref _getter);
            _items = new Dictionary<TData, TComponent>();
        }
    }
}