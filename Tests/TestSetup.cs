using System.Collections.Generic;
using Core.Abstract;
using Core.Ui;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Core.Tests
{
    // [TestFixture]
    public class TestSetup : IGameSetup, IPrebuildSetup, IPostBuildCleanup
    {
        Game game;
        
        public void Setup()
        {
            game = new Game();
            game.Setup(this);
        }

        public void Cleanup()
        {
            game = null;
        }
        
        public IEnumerable<UIRoot> UIRoots()
        {
            yield return null;
        }

        public IEnumerable<IModel> Models()
        {
            yield return null;
        }

        public IEnumerable<Service> Services()
        {
            yield return null;
        }
    }
}