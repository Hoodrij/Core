using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Tools.Bindings
{
    public abstract class ABindableBehaviour : MonoBehaviour, IBindersNotifier
    {
        private readonly List<ABinder> _attachedBinders = new List<ABinder>();
        protected bool _readyForBind;

        public bool ReadyForBind => _readyForBind;

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

        protected void RaisePropertyChanged(string propertyName)
        {
            if (_attachedBinders == null)
                return;

            for (int i = 0; i < _attachedBinders.Count; i++)
            {
                ABinder binder = _attachedBinders[i];
                Object unityObject = binder.Target as Object;

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