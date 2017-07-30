using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;

namespace D3Project.Character
{
    abstract class AI
    {
        protected Vector2 position;
        protected Timer timer;   

        public AI()
        {
            position = Vector2.Zero;
        }

        public abstract Vector2 Think(Character character); 
    }
}
