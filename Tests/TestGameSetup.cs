using System.Collections.Generic;
using Core.Abstract;
using Core.Ui;

namespace Core.Tests
{
    public class TestGameSetup : IGameSetup
    {
        public IEnumerable<UIRoot> UIRoots()
        {
            yield return null;
        }

        public IEnumerable<Model> Models()
        {
            yield return new TestModel();
        }

        public IEnumerable<Service> Services()
        {
            yield return new TestInjectService();
            yield return new TestUpdateService();
        }
    }
}