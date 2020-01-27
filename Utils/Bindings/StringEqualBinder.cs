using System;
using UnityEngine;

namespace Bindings
{
  [BindTo(typeof(String))]
  public class StringEqualBinder : ABinder
  {
#pragma warning disable 649
    [Serializable]
    private struct StringBindingPair
    {
      public String Str;
      public GameObject Obj;
    }

    [SerializeField] private StringBindingPair[] _binds;
#pragma warning restore 649

    private Func<String> _getter;

    protected override void Bind(Boolean init)
    {
      String key = _getter();

      GameObject activeItem = null;
      foreach (var i in _binds)
      {
        if (i.Str == key)
        {
          activeItem = i.Obj;
        }
        else
        {
          i.Obj.SetActive(false);
        }

        if (activeItem != null)
        {
          activeItem.SetActive(true);
        }
      }
    }

    private void Awake()
    {
      Init(ref _getter);
    }
  }
}