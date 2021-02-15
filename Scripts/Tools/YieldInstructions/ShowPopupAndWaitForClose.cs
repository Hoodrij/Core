using System;
using System.Threading.Tasks;
using Core.Tools.Observables;
using Core.Ui;
using UnityEngine;

namespace Core.Tools.Yield
{
    public class ShowPopupAndWaitForClose : CustomYieldInstruction
    {
        private bool isClosed; 
            
        public ShowPopupAndWaitForClose(Func<Task<UIView>> openFunc)
        {
            Open(openFunc);
        }

        private async void Open(Func<Task<UIView>> openFunc)
        {
            UIView view = await openFunc();
            await view.closeEvent;
            isClosed = true;
        }

        public override bool keepWaiting => !isClosed;
    }
}