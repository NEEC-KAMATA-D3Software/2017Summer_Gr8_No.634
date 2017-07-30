using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;
using D3Project.Def;

namespace D3Project.Character
{
    class ItemManager
    {
        private Timer timer;
        private Random rand;
        private int type;
        public ItemManager() { }

        public void Initialize()
        {
            timer = new Timer(0);
            rand = new Random();
        }

        public void Update(CharacterControl characterControl)
        {
            timer.addUpdate();
            //アイテム生成処理
            if (timer.Now() % 420 == 0)
            {
                type = rand.Next(0, 3);
                if (type == 0) characterControl.AddFirst(new THREEWAY(new Vector2(rand.Next(150, 650), rand.Next(150, 450)), 5.0f, characterControl));
                if (type == 1) characterControl.AddFirst(new HOMING(new Vector2(rand.Next(150, 650), rand.Next(150, 450)), 5.0f, characterControl));
                if (type == 2) characterControl.AddFirst(new BOMB(new Vector2(rand.Next(150, 650), rand.Next(150, 450)), 5.0f, characterControl));
            }
        }
    }
}
