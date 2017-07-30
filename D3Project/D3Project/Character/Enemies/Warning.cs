using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    class Warning : Character
    {
        Timer timer = new Timer(0);
        private float alpha = 1;
        public Warning(Vector2 position, ICharacterMediator mediator) : base("WARNING", "",position,0.0f, mediator)
        {
        }

        public override void Update(GameTime gameTime)
        {
            timer.addUpdate();
            alpha -= 0.02f;
            if (alpha <= 0) { alpha = 1; }

            if (timer.Now()>=240)
            {               
                isDead = true;
                timer.Initialize();
            }
        }

        public override void Hit(string type)
        {
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter,alpha);
        }
    }
}
