using System;
using System.Threading.Tasks;
using Core.Tools.Observables;
using Core.Ui;
using UnityEngine;

namespace Core.Tools.Yield
{
    public class ShowViewAndWaitForClose : CustomYieldInstruction
    {
        private bool isClosed; 
            
        public ShowViewAndWaitForClose(Func<Task<UIView>> openFunc)
        {
            Open(openFunc);
        }

        private async void Open(Func<Task<UIView>> openFunc)
        {
            UIView view = await openFunc();
            await view.CloseEvent;
            isClosed = true;
        }

        public override bool keepWaiting => !isClosed;
    }
}