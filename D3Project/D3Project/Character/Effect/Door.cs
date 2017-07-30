using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;
using D3Project.Scene;

namespace D3Project.Character
{
    class Door : Character
    {
        private Motion motion;  //モーション管理
        private Timer timer;    //切り替え時間
        WaveManager waveManager = new WaveManager();    

        public Door(Vector2 position, ICharacterMediator mediator) : base("door", "", position, 0f, mediator)
        {
            motion = new Motion();
            timer = new Timer(0.2f);

            for (int i = 0; i <= 9; i++)
            {
                motion.Add(i, new Rectangle(200 * i, 0, 200, 200));
            }

            motion.Initialize(new Range(0, 9), timer);
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
            renderer.DrawTexture(name, position, motion.DrawingRange(), Color.White);
        }
    }
}
