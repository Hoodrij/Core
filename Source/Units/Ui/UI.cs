using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Ui
{
    public class UI : Unit
    {
        [inject] Assets Assets;
        
        private readonly UILoader loader;
        private readonly UIController controller;

        public UI(IEnumerable<UIRoot> setup)
        {
            loader = new UILoader(Assets);
            controller = new UIController(loader);
            
            foreach (UIRoot root in setup)
            {
                loader.AddRoot(root);
            }
        }

        internal async Task<TView> Open<TView>(object data = null) where TView : UIView => await controller.Open<TView>(data);
        
        internal TView Get<TView>() where TView : UIView => controller.Get<TView>();
        internal UIRoot GetRoot(Type type) => loader.GetRoot(type);
        
        public void CloseAll() => controller.CloseAll();
    }
}