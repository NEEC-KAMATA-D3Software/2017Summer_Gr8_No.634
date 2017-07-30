using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    //撃破時プレイヤーへ直線状に弾発射する敵
    class AttackCrashes : Character
    {
        private AI ai;
        private int HP;
        private Random rand = new Random();    //弾追尾位置用乱数
        public AttackCrashes(AI ai, float angle, ICharacterMediator mediator) : base("enemy48", "Enemy", 24.0f, mediator)
        {
            this.ai = ai;
            this.angle = angle;
            HP = 3;
        }

        public override void Update(GameTime gameTime)
        {
            position = ai.Think(this);

            //画面外削除処理
            base.Update(gameTime);
        }

        public override void Hit(string type)
        {
            if (type == "Bullet" || type == "BOMB" || type == "Player" || type == "Barrier")
            {
                mediator.AddFirstCharacter(new BurstEffect(position, mediator));
                mediator.AddLastCharacter(new HomingEnemyBullet(position, angle + Calculate.radian(-15), rand.Next(3, 4), mediator));
                mediator.AddLastCharacter(new HomingEnemyBullet(position, angle + Calculate.radian(-7), rand.Next(4, 5), mediator));
                mediator.AddLastCharacter(new HomingEnemyBullet(position, angle + Calculate.radian(0), rand.Next(5, 6), mediator));
                mediator.AddLastCharacter(new HomingEnemyBullet(position, angle + Calculate.radian(7), rand.Next(4, 5), mediator));
                mediator.AddLastCharacter(new HomingEnemyBullet(position, angle + Calculate.radian(15), rand.Next(3, 4), mediator));
                HP--;
                if (HP <= 0) isDead = true;

                if (type == "Bullet" || type == "BOMB" || type == "Barrier") mediator.AddFirstCharacter(new SkillPoint(position, 5.0f, mediator));
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter);
        }
    }
}
