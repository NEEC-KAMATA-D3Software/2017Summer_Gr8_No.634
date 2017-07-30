using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;
using D3Project.Device;

namespace D3Project.Character
{
    class GameClearPlayer : Character
    {
        private AI ai;
        private Timer timer;
        private Sound sound;
        private float angle;

        public GameClearPlayer(AI ai, Vector2 position, Sound sound, ICharacterMediator mediator) : base("player48", "Player", position, 24.0f, mediator)
        {
            this.ai = ai;
            this.sound = sound;
            timer = new Timer(0);
            angle = Calculate.radian(-90);
        }

        public override void Update(GameTime gameTime)
        {
            position = ai.Think(this);

            timer.addUpdate();
            if (timer.Now() % 60 == 0 && timer.Now() <= 300)
            {
                mediator.AddFirstCharacter(new Bullet(position, angle, 10.0f, mediator));
            }

            if (position.X <= -24.0f) isDead = true;
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
