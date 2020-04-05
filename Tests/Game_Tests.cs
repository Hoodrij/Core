using System;
using NUnit.Framework;

namespace Core.Tests
{
    public class Game_Tests : TestFixture
    {
        [Test]
        public void Game_Created()
        {
            Assert.NotNull(game);
        }
        
        [Test]
        public void Game_Reset()
        {
            Add<TestModel>();
            
            var model = Game.Models.Get<TestModel>();
            Assert.NotNull(model);
            
            Game.Reset();
            try
            {
                model = null;
                model = Game.Models.Get<TestModel>();
            }
            catch (Exception e)
            {
                Assert.Null(model);
            }
        }
    }
}