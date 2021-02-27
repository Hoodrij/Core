using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Tools.Bindings
{
    public abstract class ABindableBehaviour : MonoBehaviour, IBindersNotifier
    {
        private readonly List<ABinder> attachedBinders = new List<ABinder>();

        protected virtual bool ReadyForBind() => true;

        public void AttachBinder(ABinder binder)
        {
            attachedBinders.Add(binder);
        }

        public void DetachBinder(ABinder binder)
        {
            attachedBinders.Remove(binder);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void Rebind()
        {
            RaisePropertyChanged("*");
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (!ReadyForBind())
                return;
            for (int i = 0; i < attachedBinders.Count; i++)
            {
                ABinder binder = attachedBinders[i];
                Object unityObject = binder.Target;

                if (unityObject != null && !unityObject)
                {
                    attachedBinders.RemoveAt(i);
                    i--;
                    continue;
                }

                binder.RebindOnPropertyChanged(propertyName);
            }
        }
    }
}