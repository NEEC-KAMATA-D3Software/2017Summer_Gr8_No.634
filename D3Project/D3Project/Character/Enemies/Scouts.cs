using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.UI;

namespace D3Project.Character
{
    //斥候
    class Scouts : Character
    {
        private AI ai;
        private int HP;

        public Scouts(AI ai, float angle, ICharacterMediator mediator) : base("Scout", "Enemy", 24.0f, mediator)
        {
            this.ai = ai;
            this.angle = angle;
            HP = 1;
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
                if (type == "BOMB") HP = 0;
                HP--;

                if (HP <= 0)
                {
                    mediator.AddFirstCharacter(new BurstEffect(position, mediator));
                    Score.add(5);
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
