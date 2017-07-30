using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    //撃破用エフェクト
    class BossBurstEffect : Character
    {
        private Motion motion;  //モーション管理
        private Timer timer;    //切り替え時間
        private Timer previousTimer; //爆発前用タイマー
        private Vector2 scale;
        private Sound sound;

        public BossBurstEffect(Vector2 position, ICharacterMediator mediator) : base("BOSS_Burst_Effect", "Effect", position, 250.0f, mediator)
        {
            sound = Sound.GetInstance();
            

            scale = new Vector2(1.0f, 1.0f);
            motion = new Motion();
            timer = new Timer(0.02f);
            previousTimer = new Timer(0);

            for (int i = 0; i <= 15; i++)
            {
                motion.Add(i, new Rectangle(500 * (i % 4), 500 * (i / 4), 500, 500));

            }
            motion.Initialize(new Range(0, 15), timer);
        }

        public override void Update(GameTime gameTime)
        {
            previousTimer.addUpdate();
            if (previousTimer.Now() < 120)
            {
                scale -= new Vector2(0.008f, 0.008f);
            }

            if(previousTimer.Now(120)) sound.PlaySE("Burst");

            if (120 <= previousTimer.Now())
            {
                motion.Update(gameTime);
                scale += new Vector2(0.05f, 0.05f);
            }

            //１回エフェクト表示させたら削除
            if (motion.OnceMotion()) isDead = true;
        }

        public override void Hit(string type)
        {

        }

        public override void Draw(Renderer renderer)
        {
            if (120 <= previousTimer.Now())
            {
                renderer.DrawTexture(name, position - new Vector2(radius, radius), motion.DrawingRange());
            }
            renderer.DrawTexture("Burst_Ring", position, new Vector2(500.0f, 500.0f), scale);
        }
    }
}