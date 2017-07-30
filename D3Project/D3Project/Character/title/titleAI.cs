using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace D3Project.Character
{
    class titleAI : AI
    {
        private List<Vector2> positions;
        private int current;

        public titleAI()
        {
            positions = new List<Vector2>()
            {
                new Vector2(70.0f, 50.0f), new Vector2(730.0f, 50.0f), new Vector2(730.0f, 934.0f), new Vector2(730.0f, 50.0f),
                new Vector2(70.0f, 50.0f), new Vector2(70.0f, 824.0f), new Vector2(740.0f, 824.0f), new Vector2(70.0f, 824.0f),
                new Vector2(730.0f, 824.0f), new Vector2(70.0f, 824.0f), new Vector2(70.0f, 50.0f), new Vector2(70.0f, 824.0f)
            };
            current = 0;
        }

        public override Vector2 Think(Character character)
        {
            character.SetPosition(ref position);
            Vector2 velocity = Vector2.Normalize(positions[current] - position);
            position += velocity * 5.0f;
            if ((positions[current] - position).Length() < 5.0f) current = (current + 1) % positions.Count;
            return position;
        }
    }
}
