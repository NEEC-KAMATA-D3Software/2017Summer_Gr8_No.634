using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;

namespace D3Project.Character
{
    //横のサインカーブ
    class HorizontalSincurveAI : AI
    {
        private float phase = 0;                 //波の位相
        private float phaseSpeed = 5.0f;         //波の位相変化量
        private float height = 60.0f;            //波の高さ
        private float center;                    //波の中心座標
        private float speed;                     //スピード

        public HorizontalSincurveAI(Vector2 position , float speed)
        {
            center = position.Y;
            this.position = position;
            this.speed = speed;
        }

        public override Vector2 Think(Character character)
        {
            phase += phaseSpeed;
            position.X += speed;
            position.Y = (float)Math.Sin(Calculate.radian(phase)) * height + center;
            return position;
        }
    }
}
