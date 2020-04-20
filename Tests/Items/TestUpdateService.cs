﻿using Core.Abstract;

namespace Core.Tests
{
    public class TestUpdateService : Service, IUpdateHandler
    {
        public int UpdateCounter;
        
        public void Update()
        {
            UpdateCounter++;
        }
        
        protected override void OnStart()
        {
            
        }
    }
}