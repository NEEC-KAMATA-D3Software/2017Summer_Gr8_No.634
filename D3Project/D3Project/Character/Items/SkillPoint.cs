using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.UI;
using D3Project.Device;

namespace D3Project.Character
{
    class SkillPoint : Character
    {
        private Sound sound;
        private float speed;  //スピード
        public SkillPoint(Vector2 position, float speed, ICharacterMediator mediator) : base("SKILL_Item16", "SKILL_Item", position, 8.0f, mediator)
        {
            sound = Sound.GetInstance();
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            //プレイヤーまでのベクトルを求める
            Vector2 velocity = mediator.GetPlayerPosition() - position;

            //プレイヤーが近づいたときにスキルポイント取得
            if (velocity.Length() <= 150)
            {
                    velocity.Normalize(); //正規化
                    position += velocity * speed;
            }
        }


        public override void Hit(string type)
        {
            if (type == "Player" || type == "damagedPlayer")
            {
                sound.PlaySE("SkillPoint");
                //スキルポイント追加
                if(!isDead) Gauge.AddValue(2.5f);
                this.isDead = true;
            }
        }
    }
}
