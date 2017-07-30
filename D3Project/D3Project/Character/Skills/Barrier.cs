using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    class Barrier : Character
    {
        private Sound sound;
        private int BarrierHP = 3;               //バリアの耐久力
        private bool isDamaged = false;          //ダメージ判定フラグ
        private Timer timer = new Timer(0.05f);  //連続判定回避用タイマー

        public Barrier(Vector2 position, ICharacterMediator mediator) : base("Barrier3", "Barrier", position, 40.0f, mediator)
        {
            sound = Sound.GetInstance();
            sound.PlaySE("Barrier");
            isDamaged = false;
        }

        public override void Update(GameTime gameTime)
        {
            timer.Update();

            if (BarrierHP == 3) name = "Barrier3";
            else if (BarrierHP == 2) name = "Barrier2";
            else if (BarrierHP == 1) name = "Barrier1";
            else if (BarrierHP <= 0) isDead = true;

            position = mediator.GetPlayerPosition();
            angle += Calculate.radian(2);
        }
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter);
        }

        public override void Hit(string type)
        {
            if (type == "EnemyBullet" || type == "BOMB")
            {
                if (!isDamaged)
                {
                    timer.Initialize();
                    BarrierHP--;
                    sound.PlaySE("BarrierDamage");
                    isDamaged = true;
                }
            }

            if (type == "Enemy") BarrierHP = 0;

            if (timer.IsTime()) isDamaged = false;
        }
    }
}
