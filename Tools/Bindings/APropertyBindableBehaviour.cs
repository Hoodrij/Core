using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tools.Bindings
{
  public abstract class APropertyBindableBehaviour : MonoBehaviour, IBindersNotifier
  {
    protected Boolean _readyForBind = false;

    private readonly List<ABinder> _attachedBinders = new List<ABinder>();

    public Boolean ReadyForBind
    {
      get { return _readyForBind; }
    }

    public void AttachBinder(ABinder binder)
    {
      _attachedBinders.Add(binder);
    }

    public void DetachBinder(ABinder binder)
    {
      _attachedBinders.Remove(binder);
    }

    public void MakeBindReadyAndRebindAll()
    {
      _readyForBind = true;
      RaisePropertyChanged("*");
    }

    public void MakeBindReady()
    {
      _readyForBind = true;
    }

    public void MakeBindUnready()
    {
      _readyForBind = false;
    }

    protected void RebindAll()
    {
      RaisePropertyChanged("*");
    }

    protected void RaisePropertyChanged(String propertyName)
    {
      if (_attachedBinders == null)
        return;

      for (var i = 0; i < _attachedBinders.Count; i++)
      {
        var binder = _attachedBinders[i];
        var unityObject = binder.Target as UnityEngine.Object;

        if (unityObject != null && !unityObject)
        {
          _attachedBinders.RemoveAt(i);
          i--;
          continue;
        }

        try
        {
          ABinder.Internal.RebindOn(binder, propertyName);
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
        }
      }
    }
  }
}