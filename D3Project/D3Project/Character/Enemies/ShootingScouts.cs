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
    //斥候
    class ShootingScouts : Character
    {
        private Timer timer;
        private int HP;
        private Sound sound;

        private List<Vector2> positions;       //移動リスト
        private int current;                   //現在の移動位置
        private Timer stopTimer;               //停止時間
        

        public ShootingScouts(Vector2 Start, Vector2 Stop, float angle, ICharacterMediator mediator) : base("ShootingScout", "Enemy", 24.0f, mediator)
        {
            this.angle = angle;
            HP = 3;

            sound = Sound.GetInstance();
            timer = new Timer(0);
            stopTimer = new Timer(5.0f);
            position = Start;
            positions = new List<Vector2>()
            {
                Start, Stop
            };
            current = 1;
        }

        public override void Update(GameTime gameTime)
        {
            //移動処理
            Vector2 velocity = Vector2.Normalize(positions[current] - position);
            if ((positions[current] - position).Length() < 5.0f)
            {
                stopTimer.Update();
                if (stopTimer.IsTime())
                {
                    current = (current + 1) % positions.Count;
                    stopTimer.Initialize();
                }
            }
            else
            {
                position += velocity * 5.0f;
            }

            timer.addUpdate();
            if (timer.Now() % 60 == 0)
            {
                mediator.AddLastCharacter(new EnemyBullet(position, angle + Calculate.radian(180), 2.0f, mediator));
            }
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
                    Score.add(15);
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
