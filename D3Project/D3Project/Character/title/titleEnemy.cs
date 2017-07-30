using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Character
{
    class titleEnemy : Character
    {
        private AI ai;
        private Vector2 previousPosition;
        public titleEnemy(AI ai, Vector2 position, ICharacterMediator mediator) : base("Scout", "", position, 24.0f, mediator)
        {
            this.ai = ai;
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

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter);
        }
    }
}
