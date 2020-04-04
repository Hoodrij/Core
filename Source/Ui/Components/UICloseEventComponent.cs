using System;
using Core.Tools.Observables;
using UnityEngine;

namespace Core.Ui
{
    public class UICloseEventComponent : MonoBehaviour
    {
        private Signal onClose = new Signal();

        internal void ListenCloseClick(Action action)
        {
            onClose.Listen(action);
        }

        // button inspector
        public void Close()
        {
            onClose.Fire();
        }
    }
}