using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D3Project.Device;
using Microsoft.Xna.Framework;

namespace D3Project.UI
{
    static class Heart
    {
        private static int HP = 3;
        static Heart() { }

        public static void Initialize() { HP = 3; }

        public static void Draw(Renderer renderer, Vector2 position)
        {
            if (HP == 3)
            {
                for (int i = 0; i <= 2; i++)
                {
                    renderer.DrawTexture("heart", position + new Vector2(i * 55 , 0));
                }
            }
            else if (HP == 2)
            {
                for (int i = 0; i <= 1; i++)
                {
                    renderer.DrawTexture("heart", position + new Vector2(i * 55, 0));
                }
            }
            else if (HP == 1)
            {
                renderer.DrawTexture("heart", position + new Vector2(0, 0));
            }

        }

        public static void Damage()
        {
            HP--;
        }

        public static int GetHP()
        {
            return HP;
        }
    }
}
