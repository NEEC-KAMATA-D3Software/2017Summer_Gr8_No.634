using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    //レーザー
    class LazerBullet : Character
    {
        private float num;         //弾数（レーザー結合用）
        private InputState input;  //操作

        public LazerBullet(InputState input, Vector2 position, float num, ICharacterMediator mediator) : base("LAZER_Bullet16", "Bullet", position, 8.0f, mediator)
        {
            this.input = input;
            this.num = num;
        }

        public override void Update(GameTime gameTime)
        {
            position = mediator.GetPlayerPosition() + Calculate.direction(mediator.GetPlayerAngle()) * num;
            if (input.IsButtonUp(Buttons.B)) isDead = true;
        }

        public override void Hit(string type)
        {

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, mediator.GetPlayerAngle(), textureCenter);
        }
    }
}
