using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Tools.Bindings
{
    [BindTo(typeof(Collection))] public class CollectionBinder : ABinder
    {
        [Tooltip("Container for items")] [SerializeField]
        private Transform _container;


        private Func<Collection> _getter;
        private Dictionary<object, GameObject> _items;


        [Tooltip("Prefab to instantiate in collection")] [SerializeField]
        private GameObject _prefab;

        [Tooltip("Methot to setup data to component from item (Item, Data, IsNew)")] [SerializeField]
        private SetupEvent _setupEvent;

        protected override void Bind()
        {
            if (_container == null || _prefab == null || _getter == null)
                return;

            _prefab.SetActive(false);

            Collection collection = _getter.Invoke();
            if (collection.IsEmpty) //clear all items
            {
                foreach (KeyValuePair<object, GameObject> item in _items)
                    Destroy(item.Value);
                _items.Clear();
            }
            else //fill items
            {
                if (collection.Comparer != null && collection.Comparer != _items.Comparer)
                    _items = new Dictionary<object, GameObject>(_items, collection.Comparer);

                int index = 0;
                HashSet<object> itemsKeys = new HashSet<object>(_items.Comparer);
                foreach (object data in collection)
                {
                    GameObject itemObj;
                    bool isNew = false;
                    //doesn't have -> create new one
                    if (!_items.TryGetValue(data, out itemObj))
                    {
                        itemObj = Instantiate(_prefab);
                        itemObj.transform.SetParent(_container, false);
                        itemObj.transform.localScale = Vector3.one;
                        _items.Add(data, itemObj);
                        isNew = true;
                    }

                    itemObj.transform.SetSiblingIndex(index);
                    if (_setupEvent != null)
                        _setupEvent.Invoke(itemObj, data, isNew);

                    if (!itemObj.activeSelf)
                        itemObj.SetActive(true);

                    itemsKeys.Add(data);
                    index++;
                }

                //clear items, who doesn't exists in collection
                object[] itemsToRemove = _items.Keys.Where(key => !itemsKeys.Contains(key)).ToArray();
                foreach (object itemKey in itemsToRemove)
                {
                    Destroy(_items[itemKey]);
                    _items.Remove(itemKey);
                }
            }
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }

        private void Awake()
        {
            Init(ref _getter);
            _items = new Dictionary<object, GameObject>();
        }

        [Serializable] private class SetupEvent : UnityEvent<GameObject, object, bool> { }
    }
}