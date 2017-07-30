using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace D3Project.Character
{
    //縦の放物線
    class VerticalParabolaAI : AI
    {
        private float speed_X;    //X方向のスピード
        private float speed_Y;    //Y方向のスピード
        private float g;          //重力加速度

        public VerticalParabolaAI(Vector2 position, float speed_X, float speed_Y, float g)
        {
            this.position = position;
            this.speed_X = speed_X;
            this.speed_Y = speed_Y;
            this.g = g;
        }

        public override Vector2 Think(Character character)
        {
            speed_Y += g;
            position.X += speed_X;
            position.Y += speed_Y;
            return position;
        }
    }
}
