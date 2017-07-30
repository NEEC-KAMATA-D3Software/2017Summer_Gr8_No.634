using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;
using D3Project.Device;

namespace D3Project.Character
{
    //レーザー
    class LazerEnemyBullet : Character
    {
        private float speed;  //弾スピード
        public LazerEnemyBullet(Vector2 position, float angle, float speed, ICharacterMediator mediator) : base("LAZER_enemyBullet32", "EnemyBullet", position, 16.0f, mediator)
        {
            this.angle = angle;
            this.speed = speed;
            this.position += Calculate.direction(angle);
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
            if (type == "Player" || type == "Barrier") isDead = true;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter);
        }
    }
}
