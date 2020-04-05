﻿using System.Collections.Generic;
using Core.Abstract;
using Core.Ui;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Core.Tests
{
    public class TestFixture
    {
        protected Game game;
        
        [SetUp]
        public void Setup()
        {
            game = new Game();
        }

        [TearDown]
        public void Cleanup()
        {
            game = null;
        }

        protected T Add<T>() where T : new()
        {
            T obj = new T();
            
            if (obj is IModel model) Game.Models.Add(model); 
            if (obj is IService service) Game.Services.Add(service);
            if (obj is UIRoot root) Game.UI.Add(root);

            return obj;
        }
    }
}