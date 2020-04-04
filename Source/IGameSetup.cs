using System.Collections.Generic;
using Core.Abstract;
using Core.Ui;

namespace Core
{
    public interface IGameSetup
    {
        IEnumerable<UIRoot> UIRoots();
        IEnumerable<IModel> Models();
        IEnumerable<Service> Services();
    }
}