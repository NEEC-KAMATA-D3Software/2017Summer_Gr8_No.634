using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Def;

namespace D3Project.Character
{
    class FakePlayer : Character
    {
        private Vector2 velocity = new Vector2(-4,0);
        public FakePlayer(Vector2 position, float angle, ICharacterMediator mediator) : base("player48", "", 24.0f, mediator)
        {
            this.position = position;
            this.angle = angle;
        }

        public override void Update(GameTime gameTime)
        {
            position = position + velocity;

            if (position.X == Screen.Width / 2) { isDead = true; }

            //画面外削除処理
            base.Update(gameTime);
        }

        public override void Hit(string type)
        {            
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter);
        }
    }
}
