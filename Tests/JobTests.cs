using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Core.Tests
{
    public class JobTests
    {
        private int i = 1;
        
        // [SetUp]
        // public void Setup()
        // {
        //     i = 2;
        // }
        //
        // [TearDown]
        // public void TearDown()
        // {
        //     i = 15;
        // }
        
        // A Test behaves as an ordinary method
        [Test]
        public void JobTestsSimplePasses()
        {
            // i = 3;
            // Use the Assert class to test conditions
            Assert.AreEqual(i, 2);
        }
        
        [UnityTest]
        public IEnumerator TestSomeAsyncThing() {
            var task = Task.Run(async () => 
            {
                await new WaitForSeconds(1);
            });
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) { throw task.Exception; }
        }

        // [Test]
        // public async Task JobTestsWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     // await new WaitForSeconds(1);
        //     Assert.True(true);
        //     
        // }
    }
}
