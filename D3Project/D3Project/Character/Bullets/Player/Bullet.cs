using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace D3Project.Character
{
    //通常弾
    class Bullet : Character
    {
        private float speed;  //スピード
        public Bullet(Vector2 position, float angle, float speed, ICharacterMediator mediator) : base("bullet16", "Bullet", position, 8.0f, mediator)
        {
            this.angle = angle;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            //移動処理
            position += new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * speed;

            //画面外削除処理
            base.Update(gameTime);
        }

        public override void Hit(string type)
        {
            if (type == "Enemy") isDead = true;
        }
    }
}
