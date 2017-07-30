using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;
using D3Project.UI;

namespace D3Project.Character
{
    //砲台兵
    class BombBatteries : Character
    {
        private AI ai;
        private int HP;
        private Timer timer;
        private Random rand;
        private Sound sound;

        public BombBatteries(AI ai, Vector2 position, float angle, ICharacterMediator mediator) : base("BombBattery", "Enemy", position, 24.0f, mediator)
        {
            this.ai = ai;
            this.angle = angle;
            HP = 8;
            timer = new Timer(0);
            rand = new Random();
            sound = Sound.GetInstance();
        }

        public override void Update(GameTime gameTime)
        {
            position = ai.Think(this);

            timer.addUpdate();

            //0秒後～9秒までに
            if (0 <= timer.Now() && timer.Now() <= 540)
            {
                //1.5秒ごとに弾発射
                if (timer.Now() % 90 == 0)
                {
                    mediator.AddFirstCharacter(new BombEnemyBullet(position, angle + Calculate.radian(rand.Next(150, 220)), 10.0f, mediator));
                }
            }

            //9秒たったらタイマー初期化
            if (timer.Now(540)) timer.Initialize();
        }

        public override void Hit(string type)
        {
            if (type == "Bullet" || type == "BOMB" || type == "Player" || type == "Barrier")
            {
                if (type == "BOMB") HP = 0;
                HP--;

                sound.PlaySE("Damege");

                if (HP <= 0)
                {
                    mediator.AddFirstCharacter(new BurstEffect(position, mediator));
                    Score.add(10);
                    isDead = true;
                }

                if (type == "Bullet" || type == "BOMB" || type == "Barrier") mediator.AddFirstCharacter(new SkillPoint(position, 5.0f, mediator));
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter);
        }
    }
}
