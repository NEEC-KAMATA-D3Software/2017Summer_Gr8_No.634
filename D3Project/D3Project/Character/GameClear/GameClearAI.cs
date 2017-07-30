using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace D3Project.Character
{
    class GameClearAI : AI
    {
        private List<Vector2> positions;
        private int current;

        public GameClearAI()
        {
            positions = new List<Vector2>()
            {
                new Vector2(825.0f, 550.0f), new Vector2(-124.0f, 550.0f)
            };
            current = 0;
        }

        public override Vector2 Think(Character character)
        {
            character.SetPosition(ref position);
            Vector2 velocity = Vector2.Normalize(positions[current] - position);
            position += velocity * 2.0f;
            if ((positions[current] - position).Length() < 5.0f) current = (current + 1) % positions.Count;
            return position;
        }
    }
}
