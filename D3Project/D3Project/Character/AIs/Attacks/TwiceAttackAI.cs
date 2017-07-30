﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;

namespace D3Project.Character
{
    //２度プレイヤーに突撃
    class TwiceAttackAI : AI
    {
        private Vector2 velocity = Vector2.Zero;  //移動量
        private float speed = 7.0f;               //スピード
        private ICharacterMediator mediator;      //プレイヤー座標取得用

        public TwiceAttackAI(Vector2 Start, ICharacterMediator mediator)
        {
            position = Start;
            this.mediator = mediator;
            timer = new Timer(0);
        }

        public override Vector2 Think(Character character)
        {
            timer.addUpdate();

            //0.5秒後と3秒後に
            if (timer.Now(30) || timer.Now(90))
            {
                //プレイヤーキャラの座標を取得
                Vector2 otherPosition = mediator.GetPlayerPosition();

                //プレイヤーまでのベクトルを求める
                velocity = Vector2.Normalize(otherPosition - position);
            }

            //１秒後に速度を止める
            if (timer.Now(60))
            {
                velocity = Vector2.Zero;
            }

            position += velocity * speed;
            return position;
        }
    }
}
