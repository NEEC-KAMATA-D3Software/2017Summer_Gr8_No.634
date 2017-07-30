using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    //爆弾用エフェクト
    class BombEffect : Character
    {
        private Motion motion;  //モーション管理
        private Timer timer;    //切り替え時間
        private Sound sound;    

        public BombEffect(Vector2 position, ICharacterMediator mediator) : base("BOMB_Effect", "BOMB", position, 80.0f, mediator)
        {
            sound = Sound.GetInstance();
            sound.PlaySE("Bomb");

            motion = new Motion();
            timer = new Timer(0.03f);

            for (int i = 0; i <= 23; i++)
            {
                motion.Add(i, new Rectangle(300 * (i % 5), 300 * (i / 5), 300, 300));
            }

            motion.Initialize(new Range(1, 23), timer);
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
            renderer.DrawTexture(name, position - new Vector2(120, 120), motion.DrawingRange(), Color.White);
        }
    }
}
