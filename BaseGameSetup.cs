using System.Collections.Generic;
using Core.Abstract;
using Core.Ui;

namespace Core
{
    public abstract class BaseGameSetup
    {
        public abstract IEnumerable<UIRoot> UIRoots();
        public abstract IEnumerable<IModel> Models();
        public abstract IEnumerable<Service> Services();
    }
}