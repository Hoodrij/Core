using NUnit.Framework;

namespace Core.Tests
{
    public class Game_Tests : TestFixture
    {
        [Test] 
        public void Create()
        {
            Assert.NotNull(game);
        }
        
        [Test] 
        public void Game_Reset()
        {
            var model = Game.Models.Get<TestModel>();
            int testValue = 123;
            model.i = testValue;
            Assert.AreEqual(model.i, testValue);
            
            Game.Reset();
            
            Assert.AreNotEqual(model.i, testValue);
        }
    }
}