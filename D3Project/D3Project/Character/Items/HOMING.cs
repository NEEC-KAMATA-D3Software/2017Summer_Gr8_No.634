using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;

namespace D3Project.Character
{
    class HOMING : Character
    {
        private Vector2 playerPosition;  //プレイヤーの位置
        private Vector2 velocity;        //移動量
        private Timer timer;

        public HOMING(Vector2 position, float deleteTime, ICharacterMediator mediator) : base("HOMING_Item32", "HOMING_Item", position, 16.0f, mediator)
        {
            angle = mediator.GetPlayerAngle();
            timer = new Timer(deleteTime);
        }

        public override void Update(GameTime gameTime)
        {
            playerPosition = mediator.GetPlayerPosition();
            timer.Update();
            if (timer.IsTime()) this.isDead = true;

            //プレイヤーまでのベクトルを求める
            velocity = Vector2.Normalize(playerPosition - position);

            //移動処理
            position += new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) + velocity * 0.5f;
            base.Update(gameTime);
        }


        public override void Hit(string type)
        {
            if (type == "Player" || type == "damagedPlayer") this.isDead = true;
        }
    }
}
