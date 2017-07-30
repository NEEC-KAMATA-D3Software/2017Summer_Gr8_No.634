using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;

namespace D3Project.Character
{
    //ホーミング弾
    class HomingBullet : Character
    {
        private float speed;            //弾スピード
        private Vector2 velocity;       //移動量
        private Character enemy;        //敵キャラ
        public HomingBullet(Vector2 position, float angle, float speed, ICharacterMediator mediator) : base("HOMING_Bullet16", "Bullet", position, 8.0f, mediator)
        {
            this.angle = angle;
            this.speed = speed;

            //初期移動量の計算
            velocity = Vector2.Normalize(new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)));

            //敵キャラの取得
            enemy = mediator.GetEnemy();
        }

        public override void Update(GameTime gameTime)
        {
            //移動量の計算
            CalcVelocity();

            //移動処理
            position += velocity * speed;

            //画面外削除処理
            base.Update(gameTime);
        }

        private void CalcVelocity()
        {
            //敵が発見できなかったら処理しない
            if (enemy == null) return;

            //敵が死んでいなかったら
            if (!enemy.IsDead())
            {
                //相手までのベクトルを求める
                velocity = Vector2.Normalize(enemy.GetPosition() - position);
            }
        }

        public override void Hit(string type)
        {
            if (type == "Enemy") isDead = true;
        }
    }
}
