using System.Collections.Generic;
using Core.Ui;

namespace Core.GameSetup
{
    public interface IGameSetup
    {
        IEnumerable<UIRoot> UIRoots();
        IEnumerable<Model> Models();
        IEnumerable<Service> Services();
    }
}