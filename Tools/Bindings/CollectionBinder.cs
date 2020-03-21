using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Tools.Bindings
{
  [BindTo(typeof(Collection))]
  public class CollectionBinder : ABinder
  {
    [Tooltip("Container for items")]
    [SerializeField]
#pragma warning disable 649
    private Transform _container;
#pragma warning restore 649

    [Tooltip("Prefab to instantiate in collection")]
    [SerializeField]
    private GameObject _prefab;

    [Tooltip("Methot to setup data to component from item (Item, Data, IsNew)")]
    [SerializeField]
#pragma warning disable 649
    private SetupEvent _setupEvent;
#pragma warning restore 649

    private Func<Collection> _getter;
    private Dictionary<object, GameObject> _items;

    [Serializable]
    private class SetupEvent : UnityEvent<GameObject, object, bool>
    {
    }

    protected override void Bind(bool init)
    {
      if (_container == null || _prefab == null || _getter == null)
        return;

      _prefab.SetActive(false);

      var collection = _getter.Invoke();
      if (collection.IsEmpty) //clear all items
      {
        foreach (var item in _items)
          Destroy(item.Value);
        _items.Clear();
      }
      else //fill items
      {
        if (collection.Comparer != null && collection.Comparer != _items.Comparer)
          _items = new Dictionary<object, GameObject>(_items, collection.Comparer);

        var index = 0;
        var itemsKeys = new HashSet<object>(_items.Comparer);
        foreach (var data in collection)
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
        var itemsToRemove = _items.Keys.Where(key => !itemsKeys.Contains(key)).ToArray();
        foreach (var itemKey in itemsToRemove)
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
  }
}