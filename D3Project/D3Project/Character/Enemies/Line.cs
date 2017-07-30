using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Character
{
    class Line : Character
    {
        Timer timer = new Timer(0);
        private float speed; 
        public Line(float speed,Vector2 position, ICharacterMediator mediator) : base("LINE1", "",position,0.0f, mediator)
        {
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            position += new Vector2(speed, 0);
            timer.addUpdate();
            if (timer.Now() >= 240)
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
            renderer.DrawTexture(name, position, angle, textureCenter);
        }
    }
}
