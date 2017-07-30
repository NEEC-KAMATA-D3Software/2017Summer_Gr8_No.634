using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.UI
{
    static class Score
    {
        private static int score;

        static Score() { }

        public static void Initialize()
        {
            score = 0;
        }

        public static void add()
        {
            score++;
        }

        public static void add(int value)
        {
            score += value;
        }

        public static int GetScore()
        {
            return score;
        }

        public static void Draw(Renderer renderer, Vector2 position, Color color)
        {
            renderer.DrawNumber("number", position, score, color);
        }
    }
}
