using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Character
{
    //爆弾
    class BombBullet : Character
    {
        private float speed;  //スピード
        private Sound sound;  //音
        public BombBullet(Vector2 position, float angle, float speed, ICharacterMediator mediator) : base("BOMB_Bullet16", "Bullet", position, 8.0f, mediator)
        {
            sound = Sound.GetInstance();
            this.angle = angle;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            //移動処理            
            speed -= 0.2f;
            position += new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * speed;
            
            //速度が0未満になったら、爆弾生成
            if (speed < 0)
            {
                mediator.AddFirstCharacter(new BombEffect(position, mediator));
                isDead = true;
            }

            //画面外削除処理
            base.Update(gameTime);
        }

        public override void Hit(string type)
        {
            //敵に当たったら、爆弾生成
            if (type == "Enemy")
            {
                mediator.AddFirstCharacter(new BombEffect(position, mediator));
                isDead = true;
            }
        }
    }
}
