using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    class InvincibleEffect : Character
    {
        private Motion motion;
        private float alpha;
        private Timer timer;
        private Timer SkillTimer;
        private Sound sound;

        public InvincibleEffect(Vector2 position, ICharacterMediator mediator) : base("Invincible_Effect", "INVINCIBLEEffect", position, 50.0f, mediator)
        {
            sound = Sound.GetInstance();
            sound.PlaySE("Invincible");

            alpha = 1.0f;
            motion = new Motion();
            timer = new Timer(0.02f);
            SkillTimer = new Timer(5.0f); //スキル持続時間

            for (int i = 0; i <= 29; i++)
            {
                motion.Add(i, new Rectangle(100 * (i % 10), 100 * (i / 10), 100, 100));
            }

            motion.Initialize(new Range(0, 29), timer);
        }

        public override void Update(GameTime gameTime)
        {
            SkillTimer.Update();
            motion.Update(gameTime);

            if (SkillTimer.IsTime())
            {
                SkillTimer.Initialize();
                isDead = true;
            }

            if(SkillTimer.Now() < 120)
            {
                if (SkillTimer.Now() % 15 == 0)
                {
                    alpha = 0.0f;
                }

                else if(SkillTimer.Now() % 5 == 0)
                {
                    alpha = 1.0f;
                }
            }

        }

        public override void Hit(string type)
        {

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, mediator.GetPlayerPosition() - new Vector2(radius, radius), motion.DrawingRange(), alpha);
        }
    }
}

