using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Character
{
    class GameClearEnemy : Character
    {
        private Sound sound;
        public GameClearEnemy(Sound sound, Vector2 position, ICharacterMediator mediator) : base("Scout", "Enemy", position, 24.0f, mediator)
        {
            this.sound = sound;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Hit(string type)
        {
            if(type == "Bullet")
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
