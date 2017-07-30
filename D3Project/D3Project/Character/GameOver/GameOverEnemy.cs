using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Character
{
    class GameOverEnemy : Character
    {
        private AI ai;
        private Sound sound;
        private Vector2 previousPosition;
        public GameOverEnemy(AI ai, Sound sound, Vector2 position, ICharacterMediator mediator) : base("Scout", "Enemy", position, 24.0f, mediator)
        {
            this.ai = ai;
            this.sound = sound;
            previousPosition = position;
        }

        public override void Update(GameTime gameTime)
        {
            previousPosition = position;
            position = ai.Think(this);
            Vector2 difference= previousPosition - position;
            angle = (float)Math.Atan2(difference.X, -difference.Y);
        }

        public override void Hit(string type)
        {
            if(type == "Player")
            {
                mediator.AddFirstCharacter(new BurstEffect(position, mediator));
                isDead = true;
            }

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter);
        }
    }
}
