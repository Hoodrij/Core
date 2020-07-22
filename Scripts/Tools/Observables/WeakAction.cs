﻿﻿using System;
using System.Reflection;
using UnityEngine;

namespace Core.Tools.Observables
{
    internal class WeakAction<T>
    {
        public bool IsAlive => weakReference.IsAlive && weakReference?.Target != null 
                                && IsAliveAsMonoBeh();
        
        private readonly WeakReference weakReference;
        private readonly MethodInfo method;
        
        public WeakAction(Action<T> action)
        {
            weakReference = new WeakReference(action.Target);
            method = action.Method;
        }

        public void Invoke(T t)
        {
            if (IsActiveAsMonoBeh())
                method.Invoke(weakReference.Target, new object[] { t });
        }

        public bool Equals(Action<T> action) => weakReference.Target == action.Target;
        
        private bool IsAliveAsMonoBeh() => !(weakReference.Target is MonoBehaviour mono) || mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(weakReference.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
    
    internal class WeakAction
    {
        public bool IsAlive => weakReference.IsAlive && weakReference?.Target != null 
                                && IsAliveAsMonoBeh();
        
        private readonly WeakReference weakReference;
        private readonly MethodInfo method;
        
        public WeakAction(Action action)
        {
            weakReference = new WeakReference(action.Target);
            method = action.Method;
        }

        public void Invoke()
        {
            if (IsActiveAsMonoBeh())
                method.Invoke(weakReference.Target, null);
        }

        public bool Equals(Action action) => weakReference.Target == action.Target;
        
        private bool IsAliveAsMonoBeh() => !(weakReference.Target is MonoBehaviour mono) || mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(weakReference.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
}