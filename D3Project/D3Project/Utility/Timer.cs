using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D3Project.Utility
{
    class Timer
    {
        private float currentTime; //現在の時間
        private float limitTime;   //制限時間

        public Timer()
        {
            limitTime = 60.0f;
            Initialize();
            
        }

        public Timer(float second)
        {
            limitTime = 60.0f * second;
            Initialize();
        }

        public void Initialize()
        {
            currentTime = limitTime;
        }

        public void Update()
        {
            currentTime = (currentTime <= 0.0f) ? (0.0f) : (currentTime -= 1.0f);
        }

        public void addUpdate()
        {
            currentTime += 1.0f;
        }

        public float Now()
        {
            return currentTime;
        }

        public bool Now(float time)
        {
            return currentTime == time;
        }

        public bool IsTime()
        {
            return currentTime <= 0.0f;
        }

        public void Change(float limitTime)
        {
            this.limitTime = limitTime;
            Initialize();
        }

        public float Rate()
        {
            return currentTime / limitTime;
        }
    }
}
