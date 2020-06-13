using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Tools.Bindings
{
    public abstract class ABindableBehaviour : MonoBehaviour, IBindersNotifier
    {
        private readonly List<ABinder> _attachedBinders = new List<ABinder>();

        public bool ReadyForBind { get; private set; }

        public void AttachBinder(ABinder binder)
        {
            _attachedBinders.Add(binder);
        }

        public void DetachBinder(ABinder binder)
        {
            _attachedBinders.Remove(binder);
        }

        public void SetBindReady(bool isReady)
        {
            ReadyForBind = isReady;
        }

        protected void RebindAll()
        {
            ReadyForBind = true;
            RaisePropertyChanged("*");
        }

        private void RaisePropertyChanged(string propertyName)
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

                binder.RebindOnPropertyChanged(propertyName);
            }
        }
    }
}