using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;

namespace D3Project.Character
{
    class LAZER : Character
    {
        private Timer timer;
        public LAZER(Vector2 position, float deleteTime, ICharacterMediator mediator) : base("LAZER_Item32", "LAZER_Item", position, 16.0f, mediator)
        {
            timer = new Timer(deleteTime);
        }

        public override void Update(GameTime gameTime)
        {
            timer.Update();
            if (timer.IsTime()) this.isDead = true;
            base.Update(gameTime);
        }


        public override void Hit(string type)
        {
            if (type == "Player" || type == "damagedPlayer") this.isDead = true;
        }
    }
}
