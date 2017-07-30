using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Character
{
    class GameOverPlayer : Character
    {
        private Sound sound;

        public GameOverPlayer(Vector2 position, Sound sound, ICharacterMediator mediator) : base("player48", "Player", position, 24.0f, mediator)
        {
            this.sound = sound;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Hit(string type)
        {
            if(type == "Enemy")
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
