using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Abstract;
using Core.Ui;
using NUnit.Framework;

namespace Core.Tests
{
    public class TestFixture
    {
        protected static Game game;
        
        [SetUp]
        public void Setup()
        {
            if (game != null) 
                return;
            game = new Game(new TestGameSetup());
        }

        [TearDown]
        public void Cleanup()
        {
            Game.Reset();
        }

        protected T GetService<T>() where T : Service
        {
            Type type = Game.Services.GetType();
            FieldInfo fieldInfo = type.GetField("services", BindingFlags.NonPublic | BindingFlags.Instance);
            object value = fieldInfo.GetValue(Game.Services);
            return (value as List<Service>).Find(service => service is T) as T;
        }
    }
}