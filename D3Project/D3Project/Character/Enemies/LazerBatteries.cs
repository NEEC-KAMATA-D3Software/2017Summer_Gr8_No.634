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
    class LazerBatteries : Character
    {
        private AI ai;
        private int HP;
        private Timer timer;
        private Sound sound;

        public LazerBatteries(AI ai, Vector2 position, float angle, ICharacterMediator mediator) : base("Lazer", "Enemy", position, 24.0f, mediator)
        {
            this.ai = ai;
            this.angle = angle;
            HP = 8;
            sound = Sound.GetInstance();

            timer = new Timer(0);
        }

        public override void Update(GameTime gameTime)
        {
            position = ai.Think(this);

            timer.addUpdate();

            //3秒後～6秒後まで
            if (180 <= timer.Now() && timer.Now() <= 360)
            {
                //レーザー発射
                if (timer.Now() % 1 == 0)
                {
                    mediator.AddFirstCharacter(new LazerEnemyBullet(position, angle + Calculate.radian(180), 5.0f, mediator));
                }
            }

            //9秒後にタイマー初期化
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
