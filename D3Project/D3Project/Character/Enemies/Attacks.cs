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
    //突撃兵
    class Attacks : Character
    {
        private AI ai;
        private int HP;
        private float alpha;                //透過率
        private Timer timer;                //当たり判定回避用タイマー
        private Vector2 previousPosition;   //直前の座標取得用

        public Attacks(AI ai, ICharacterMediator mediator) : base("Attack", "", 24.0f, mediator)
        {
            this.ai = ai;
            HP = 1;
            alpha = 0.0f;
            angle = Calculate.radian(180);
            timer = new Timer(0);
        }

        public override void Update(GameTime gameTime)
        {
            timer.addUpdate();

            if (timer.Now() < 60)
            {
                alpha += 0.04f;
                if (alpha > 1.0f) alpha = 1.0f;
                angle = Calculate.radian(180);
            }

            if (60 <= timer.Now())
            {
                ChangeType("Enemy");
            }

            previousPosition = position;

            position = ai.Think(this);

            //座標の差から角度を計算
            Vector2 difference = previousPosition - position;
            if (difference != Vector2.Zero) angle = (float)Math.Atan2(difference.X, -difference.Y);

            //画面外削除処理
            base.Update(gameTime);
        }

        public override void Hit(string type)
        {
            if (typeName() == "Enemy")
            {
                if (type == "Bullet" || type == "BOMB" || type == "Player" || type == "Barrier")
                {
                    if (type == "BOMB") HP = 0;
                    HP--;

                    if (HP <= 0)
                    {
                        mediator.AddFirstCharacter(new BurstEffect(position, mediator));
                        Score.add(7);
                        isDead = true;
                    }

                    if (type == "Bullet" || type == "BOMB" || type == "Barrier") mediator.AddFirstCharacter(new SkillPoint(position, 5.0f, mediator));
                }
            }
        }

        public override void Draw(Renderer renderer)
        {
            if(1 < timer.Now()) renderer.DrawTexture(name, position, angle, textureCenter, alpha);
        }

    }
}
