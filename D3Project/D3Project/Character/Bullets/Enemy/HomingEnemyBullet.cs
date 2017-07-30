using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;
namespace D3Project.Character
{
    //ホーミング弾
    class HomingEnemyBullet : Character
    {
        private float speed;             //スピード
        private Vector2 playerPosition;  //プレイヤーの位置
        private Vector2 velocity;        //移動量
        private Timer timer;             //１回処理用タイマー

        public HomingEnemyBullet(Vector2 position,  float angle, float speed, ICharacterMediator mediator) : base("enemyBullet16", "EnemyBullet", position, 8.0f, mediator)
        {
            this.speed = speed;
            this.angle = angle;
            playerPosition = mediator.GetPlayerPosition();
            timer = new Timer(0);
        }

        public override void Update(GameTime gameTime)
        {
            timer.addUpdate();

            if (timer.Now(1))
            {
                //プレイヤーまでのベクトルを求める
                velocity = Vector2.Normalize(playerPosition - position);
            }

            //移動処理
            position += new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) + velocity * speed;

            //画面外削除処理
            base.Update(gameTime);
        }

        public override void Hit(string type)
        {
            if (type == "Player" || type == "Barrier") isDead = true;
        }
    }
}
