using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace D3Project.Character
{
    //直線移動
    class StraightAI : AI
    {
        private Vector2 velocity;              //移動量
        private Vector2 Start;                 //開始位置
        private Vector2 End;                   //終了位置
        private float speed = 3.0f;            //スピード

        public StraightAI(Vector2 Start, Vector2 End)
        {
            position = Start;
            this.Start = Start;
            this.End = End;
        }

        public override Vector2 Think(Character character)
        {
            velocity = Vector2.Normalize(End - Start);
            position += velocity * speed;
            return position;
        }
    }
}
