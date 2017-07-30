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
    class BurstEffect : Character
    {
        private Motion motion;  //モーション管理
        private Timer timer;    //切り替え時間
        private Sound sound;     

        public BurstEffect(Vector2 position, ICharacterMediator mediator) : base("Burst_Effect", "Effect", position, 64.0f, mediator)
        {
            sound = Sound.GetInstance();
            sound.PlaySE("Burst");

            motion = new Motion();
            timer = new Timer(0.02f);

            for (int i = 0; i <= 16; i++)
            {
                motion.Add(i, new Rectangle(128 * (i % 7), 128 * (i / 7), 128, 128));

            }
            motion.Initialize(new Range(0, 16), timer);
        }

        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);

            //１回エフェクト表示させたら削除
            if (motion.OnceMotion()) isDead = true;
        }

        public override void Hit(string type)
        {

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position - new Vector2(radius, radius), motion.DrawingRange());
        }
    }
}