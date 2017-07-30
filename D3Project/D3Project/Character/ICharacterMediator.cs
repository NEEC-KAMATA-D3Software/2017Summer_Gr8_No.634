using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace D3Project.Character
{
    interface ICharacterMediator
    {
        void AddFirstCharacter(Character character);
        void AddLastCharacter(Character character);
        Vector2 GetPlayerPosition();
        float GetPlayerAngle();
        Character GetEnemy();
    }
}
