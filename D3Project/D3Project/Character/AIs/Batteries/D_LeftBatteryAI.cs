using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;


namespace D3Project.Character
{
    //左下から出てきて止まる
    class D_LeftBatteryAI : AI
    {
        private Vector2 velocity;              //移動量
        private float speed = 0.7f;            //スピード

        public D_LeftBatteryAI()
        {
            velocity = new Vector2(1.0f, -1.0f);
            timer = new Timer(0);
        }

        public override Vector2 Think(Character character)
        {
            //キャラクタの座標の取得
            character.SetPosition(ref position);

            timer.addUpdate();

            //0秒後～3秒まで進む
            if (0 <= timer.Now() && timer.Now() <= 180)
            {
                position += velocity * speed;
            }

            //6秒後～9秒まで退く
            if (360 <= timer.Now() && timer.Now() <= 540)
            {
                position -= velocity * speed;
            }

            //9秒後タイマーを初期化
            if (timer.Now() == 540)
            {
                timer.Initialize();
            }

            return position;

        }
    }
}
